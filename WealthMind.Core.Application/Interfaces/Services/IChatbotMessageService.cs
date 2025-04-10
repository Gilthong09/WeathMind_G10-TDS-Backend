using WealthMind.Core.Application.ViewModels.ChatbotMessage;
using WealthMind.Core.Application.ViewModels.FinancialGoal;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IChatbotMessageService : IGenericService<SaveChatbotMessageViewModel, ChatbotMessageViewModel, ChatbotMessage>
    {
        Task<ChatbotMessageViewModel> AddAsync(SaveChatbotMessageViewModel viewModel);
        Task<ChatbotMessageViewModel> GetByIdAsync(string id);
        Task<List<ChatbotMessageViewModel>> GetBySessionIdAsync(string sessionId);
        Task DeleteAsync(string id);
    }
}
