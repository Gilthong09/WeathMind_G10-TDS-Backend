using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class ChatbotMessage: AuditableBaseEntity
    {
        public string SessionId { get; set; }
        public string UserMessage { get; set; } = null!;
        public string BotResponse { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ChatbotSession Session { get; set; } = null!;
    }
}
