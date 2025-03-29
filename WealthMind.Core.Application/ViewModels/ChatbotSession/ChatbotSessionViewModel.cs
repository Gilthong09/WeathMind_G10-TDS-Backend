using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.ViewModels.ChatbotSession
{
    public class ChatbotSessionViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public string ChatName { get; set; } = "active";
        public string Status { get; set; } = "active";

        // Relaciones
        public ICollection<WealthMind.Core.Domain.Entities.ChatbotMessage> Messages { get; set; } = new List<WealthMind.Core.Domain.Entities.ChatbotMessage>();
    }
}
