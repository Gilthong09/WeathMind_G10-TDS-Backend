namespace WealthMind.Core.Application.ViewModels.ChatbotMessage
{
    public class ChatbotMessageViewModel
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
