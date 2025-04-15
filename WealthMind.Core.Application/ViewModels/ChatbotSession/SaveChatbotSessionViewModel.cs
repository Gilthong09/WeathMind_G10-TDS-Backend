using Microsoft.AspNetCore.Mvc;

namespace WealthMind.Core.Application.ViewModels.ChatbotSession
{
    public class SaveChatbotSessionViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public string ChatName { get; set; } = "active";
        public string Status { get; set; } = "active";

        // Relaciones
        //[BindProperty]
        //public ICollection<WealthMind.Core.Domain.Entities.ChatbotMessage>? Messages { get; set; } = new List<WealthMind.Core.Domain.Entities.ChatbotMessage>();

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }

}
