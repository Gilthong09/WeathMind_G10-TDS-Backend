namespace WealthMind.Core.Application.ViewModels.ChatbotMessage
{
    public class SaveChatbotMessageViewModel
    {
        public string SessionId { get; set; }
        public string UserMessage { get; set; } = null!;
        public string BotResponse { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Relaciones
        public WealthMind.Core.Domain.Entities.ChatbotSession Session { get; set; } = null!;
    }
   
}
