using WealthMind.Core.Application.ViewModels.ChatbotSession;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IChatbotSessionService : IGenericService<SaveChatbotSessionViewModel, ChatbotSessionViewModel, ChatbotSession>
    {
        Task<List<ChatbotSessionViewModel>> GetAllSessionsWithMessagesAsync();
        Task<List<ChatbotSessionViewModel>> GetAllActiveSessionsByUserIdAsync(string userId);
    }
}
