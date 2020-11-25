using System;

namespace QuoteBot.DatabaseLayer.Entities
{
    public class Quote
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public long ReporterId { get; set; }
        public long ServerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
    }
}
