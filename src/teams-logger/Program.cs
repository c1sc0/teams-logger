using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using teams_logger.Services;

namespace teams_logger;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        await using var serviceProvider = services.BuildServiceProvider();
        using var app = serviceProvider.GetService<TeamsLogger>();


        await app.RunAsync();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(builder => { builder.AddConsole().AddDebug(); }).AddTransient<TeamsLogger>();
        services.AddScoped<IConfiguration>(_ =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build());
    }
}