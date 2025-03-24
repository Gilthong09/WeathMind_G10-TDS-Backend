using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.ChatbotMessage;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class ChatbotMessageService : GenericService<SaveChatbotMessageViewModel, ChatbotMessageViewModel, ChatbotMessage>, IChatbotMessageService
    {
        private readonly IChatbotMessageRepository _chatbotMessageRepository;
        private readonly IMapper _mapper;

        public ChatbotMessageService(IChatbotMessageRepository chatbotMessageRepository, IMapper mapper) : base(chatbotMessageRepository, mapper)
        {
            _chatbotMessageRepository = chatbotMessageRepository;
            _mapper = mapper;
        }

    }
}
