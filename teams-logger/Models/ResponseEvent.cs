using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace teams_logger.Models
{
    public class ResponseEvent
    {
        [JsonProperty("next")]
        public Uri Next { get; set; }

        [JsonProperty("eventMessages")]
        public List<EventMessage> EventMessages { get; set; }
    }
}