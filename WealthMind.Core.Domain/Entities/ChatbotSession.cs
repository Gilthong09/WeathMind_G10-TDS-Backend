using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class ChatbotSession : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public string ChatName { get; set; }
        public string Status { get; set; } = "active";

        // Relaciones
        public ICollection<ChatbotMessage> Messages { get; set; } = new List<ChatbotMessage>();
    }
}
