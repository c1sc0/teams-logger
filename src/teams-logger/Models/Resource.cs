using System;
using Newtonsoft.Json;

namespace teams_logger.Models
{
    public class Resource
    {
        [JsonProperty("clientmessageid")]
        public string Clientmessageid { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("from")]
        public Uri From { get; set; }

        [JsonProperty("imdisplayname")]
        public string Imdisplayname { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("messagetype")]
        public string Messagetype { get; set; }

        [JsonProperty("originalarrivaltime")]
        public DateTimeOffset Originalarrivaltime { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("sequenceId")]
        public long SequenceId { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("composetime")]
        public DateTimeOffset Composetime { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("conversationLink")]
        public Uri ConversationLink { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("contenttype")]
        public string Contenttype { get; set; }

        [JsonProperty("threadtype")]
        public string Threadtype { get; set; }
    }
}