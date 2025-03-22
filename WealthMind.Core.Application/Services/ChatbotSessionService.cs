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
    }
}
