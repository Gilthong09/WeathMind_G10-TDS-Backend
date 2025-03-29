using WealthMind.Core.Application.ViewModels.ChatbotMessage;

namespace WealthMind.Core.Application.ViewModels.ChatbotSession
{
    public class ChatbotSessionViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public string ChatName { get; set; }
        public string Status { get; set; }
        public List<ChatbotMessageViewModel> Messages { get; set; }
    }
}
