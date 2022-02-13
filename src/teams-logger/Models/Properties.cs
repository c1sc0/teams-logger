using Newtonsoft.Json;

namespace teams_logger.Models
{
    public class Properties
    {
        [JsonProperty("importance")]
        public string Importance { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("edittime")]
        public string Edittime { get; set; }
    }
}