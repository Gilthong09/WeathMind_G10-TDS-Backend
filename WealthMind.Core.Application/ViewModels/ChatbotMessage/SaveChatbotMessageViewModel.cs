namespace WealthMind.Core.Application.ViewModels.ChatbotMessage
{
    public class SaveChatbotMessageViewModel
    {
        public string SessionId { get; set; }
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
