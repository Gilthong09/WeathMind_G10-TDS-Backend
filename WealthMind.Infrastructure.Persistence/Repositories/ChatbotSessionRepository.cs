using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class ChatbotSessionRepository : GenericRepository<ChatbotSession>, IChatbotSessionRepository
    {
        private readonly ApplicationContext _dbContext;

        public ChatbotSessionRepository(ApplicationContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<List<ChatbotSession>> GetAllActiveSessionsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
