using WealthMind.Core.Application.ViewModels.ChatbotMessage;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IChatbotMessageService
    {
        Task<ChatbotMessageViewModel> AddAsync(SaveChatbotMessageViewModel viewModel);
        Task<ChatbotMessageViewModel> GetByIdAsync(string id);
        Task<List<ChatbotMessageViewModel>> GetBySessionIdAsync(string sessionId);
        Task DeleteAsync(string id);
    }
}
