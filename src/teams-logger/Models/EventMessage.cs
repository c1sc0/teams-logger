using System;
using Newtonsoft.Json;

namespace teams_logger.Models
{
    public class EventMessage
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("resourceLink")]
        public Uri ResourceLink { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("resource")]
        public Resource Resource { get; set; }
    }
}