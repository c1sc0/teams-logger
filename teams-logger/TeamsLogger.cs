using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using teams_logger.Models;

namespace teams_logger
{
    public class TeamsLogger : IDisposable
    {
        private CancellationTokenSource _cancellationToken;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public TeamsLogger(ILogger<TeamsLogger> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _cancellationToken = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _logger.LogInformation("Teams logger service stopped.");
            _cancellationToken.Cancel();
        }

        public async Task RunAsync(CancellationTokenSource cts = null)
        {
            if (cts != null)
            {
                _cancellationToken = cts;
            }

            var (token, requestUri, initialQuery) = LoadConfig();

            _logger.LogInformation("Starting teams logging service.");
            using var client = new HttpClient();
            var request = PrepareHttpRequestMessage(requestUri, token, initialQuery);

            await using var context = new LoggerContext();
            await context.Database.EnsureCreatedAsync();

            while (true)
            {
                var result = await client.SendAsync(request);
                var text = await result.Content.ReadAsStringAsync();
                var response = ConvertTextToResponseEvent(text);
                var query = GetNextQuery(response);
                request = PrepareHttpRequestMessage(requestUri, token, query.Value);

                if (ResponseIsNotNull(response))
                {
                    var loggerItem = await StoreMessage(response, text, context);
                    _logger.LogInformation($"From ({loggerItem.Timestamp}): {loggerItem.From}: {loggerItem.Message}");
                }

                if (_cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Teams logger service stopping...");
                    return;
                }
            }
        }

        private (string token, string requestUri, string initialQuery) LoadConfig()
        {
            var token = _configuration["Token"];
            var requestUri = _configuration["RequestUri"];
            var initialQuery = _configuration["Query"];
            return (token, requestUri, initialQuery);
        }

        private static bool ResponseIsNotNull(ResponseEvent? response) => response.EventMessages != null;

        private static HttpRequestMessage PrepareHttpRequestMessage(string requestUri, string token,
            string initialQuery)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("authentication", token);
            request.Headers.Add("x-ms-query-params", initialQuery);
            return request;
        }

        private static Match GetNextQuery(ResponseEvent? response)
        {
            return Regex.Match(response.Next.Query, "(?<=\\?).*");
        }

        private static ResponseEvent? ConvertTextToResponseEvent(string text)
        {
            return JsonConvert.DeserializeObject<ResponseEvent>(text);
        }

        private static async Task<LogItem> StoreMessage(ResponseEvent? response, string text, LoggerContext context)
        {
            var loggerItem = new LogItem
            {
                From = response.EventMessages.FirstOrDefault()?.Resource.Imdisplayname,
                Message = response.EventMessages.FirstOrDefault()?.Resource.Content,
                Timestamp = DateTime.Now,
                Raw = text
            };
            context.Add(loggerItem);
            await context.SaveChangesAsync();
            return loggerItem;
        }
    }
}