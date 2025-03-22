using WealthMind.Core.Application.ViewModels.ChatbotMessage;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IChatbotMessageService : IGenericService<SaveChatbotMessageViewModel, ChatbotMessageViewModel, ChatbotMessage>
    {

    }
}
