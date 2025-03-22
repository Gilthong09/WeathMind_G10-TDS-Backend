namespace WealthMind.Core.Application.ViewModels.ChatbotSession
{
    public class SaveChatbotSessionViewModel
    {
        public string UserId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public string ChatName { get; set; } = "active";
        public string Status { get; set; } = "active";

        // Relaciones
        public ICollection<ChatbotMessage> Messages { get; set; } = new List<ChatbotMessage>();
    }
   
}
