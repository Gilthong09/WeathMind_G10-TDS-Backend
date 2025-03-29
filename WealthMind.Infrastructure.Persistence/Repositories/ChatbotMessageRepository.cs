using Microsoft.EntityFrameworkCore;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class ChatbotMessageRepository : GenericRepository<ChatbotMessage>, IChatbotMessageRepository
    {
        private readonly ApplicationContext _dbContext;

        public ChatbotMessageRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ChatbotMessage>> GetBySessionIdAsync(string sessionId)
        {
            return await _dbContext.ChatbotMessages
                .Where(m => m.SessionId == sessionId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
