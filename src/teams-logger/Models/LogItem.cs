using System;

namespace teams_logger.Models
{
    public class LogItem
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string Raw { get; set; }

    }
}