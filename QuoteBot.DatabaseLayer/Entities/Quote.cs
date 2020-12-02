using System;
using System.Collections.Generic;

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

        public virtual ICollection<QuoteAttachment> QuoteAttachments { get; set; } = new HashSet<QuoteAttachment>();
    }

    public class QuoteAttachment
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public string Url { get; set; }
        
        public virtual Quote Quote { get; set; }
    }
}
