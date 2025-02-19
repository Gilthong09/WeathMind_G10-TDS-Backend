using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Domain.Entities
{
    public class ChatbotSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public string ChatName { get; set; } = "active";
        public string Status { get; set; } = "active"; // Puede ser "active", "closed"

        // Relaciones
        public ICollection<ChatbotMessage> Messages { get; set; } = new List<ChatbotMessage>();
    }
}
