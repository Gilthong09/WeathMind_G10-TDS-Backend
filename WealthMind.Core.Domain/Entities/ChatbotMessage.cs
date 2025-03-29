using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class ChatbotMessage : AuditableBaseEntity
    {
        public string SessionId { get; set; }
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Make the navigation property optional
        public virtual ChatbotSession? Session { get; set; }
    }
}
