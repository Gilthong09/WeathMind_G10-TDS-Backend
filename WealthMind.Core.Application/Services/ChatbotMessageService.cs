using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.ChatbotMessage;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class ChatbotMessageService : IChatbotMessageService
    {
        private readonly IChatbotMessageRepository _messageRepository;
        private readonly IChatbotSessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public ChatbotMessageService(
            IChatbotMessageRepository messageRepository,
            IChatbotSessionRepository sessionRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public async Task<ChatbotMessageViewModel> AddAsync(SaveChatbotMessageViewModel viewModel)
        {
            // Validate session exists
            var session = await _sessionRepository.GetByIdAsync(viewModel.SessionId);
            if (session == null)
                throw new ArgumentException($"Chat session with ID {viewModel.SessionId} not found.");

            // Create and save the message
            var message = _mapper.Map<ChatbotMessage>(viewModel);
            message.Timestamp = DateTime.UtcNow;
            
            await _messageRepository.AddAsync(message);
            
            return _mapper.Map<ChatbotMessageViewModel>(message);
        }

        public async Task<ChatbotMessageViewModel> GetByIdAsync(string id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            return _mapper.Map<ChatbotMessageViewModel>(message);
        }

        public async Task<List<ChatbotMessageViewModel>> GetBySessionIdAsync(string sessionId)
        {
            var messages = await _messageRepository.GetBySessionIdAsync(sessionId);
            return _mapper.Map<List<ChatbotMessageViewModel>>(messages);
        }

        public async Task DeleteAsync(string id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message == null)
                throw new ArgumentException($"Message with ID {id} not found.");
        
            await _messageRepository.DeleteAsync(message);
        }
    }
}
