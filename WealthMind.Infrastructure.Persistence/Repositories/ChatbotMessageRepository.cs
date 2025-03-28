using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class ChatbotMessageRepository : GenericRepository<ChatbotMessage>, IChatbotMessageRepository
    {
        private readonly ApplicationContext _dbContext;

        public ChatbotMessageRepository(ApplicationContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<List<ChatbotMessage>> GetMessagesBySessionIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
