using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.ChatbotSession;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class ChatbotSessionService : GenericService<SaveChatbotSessionViewModel, ChatbotSessionViewModel, ChatbotSession>, IChatbotSessionService
    {
        private readonly IChatbotSessionRepository _chatbotSessionRepository;
        private readonly IMapper _mapper;

        public ChatbotSessionService(IChatbotSessionRepository chatbotSessionRepository, IMapper mapper) : base(chatbotSessionRepository, mapper)
        {
            _chatbotSessionRepository = chatbotSessionRepository;
            _mapper = mapper;
        }

        public async Task<List<ChatbotSessionViewModel>> GetAllSessionsWithMessagesAsync()
        {
            var sessions = await _chatbotSessionRepository.GetAllAsync();
            return _mapper.Map<List<ChatbotSessionViewModel>>(sessions);
        }

        public async Task<List<ChatbotSessionViewModel>> GetAllActiveSessionsByUserIdAsync(string userId)
        {
            var sessions = await _chatbotSessionRepository.GetAllActiveSessionsByUserIdAsync(userId);
            return _mapper.Map<List<ChatbotSessionViewModel>>(sessions);
        }

        public async Task<ChatbotSessionViewModel> Update(SaveChatbotSessionViewModel viewModel, string id)
        {
            var existingSession = await _chatbotSessionRepository.GetByIdAsync(id);
            
            if (existingSession == null)
            {
                throw new KeyNotFoundException($"ChatbotSession with ID {id} was not found");
            }
            
            existingSession.ChatName = viewModel.ChatName;
            existingSession.Status = viewModel.Status;
            existingSession.LastModifiedBy = viewModel.UserId;
            existingSession.LastModified = DateTime.UtcNow;

            await _chatbotSessionRepository.UpdateAsync(existingSession, id);

            return _mapper.Map<ChatbotSessionViewModel>(existingSession);
        }
    }
}
