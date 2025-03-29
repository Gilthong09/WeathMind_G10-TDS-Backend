using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class ChatbotSessionRepository : GenericRepository<ChatbotSession>, IChatbotSessionRepository
    {
        private readonly ApplicationContext _dbContext;

        public ChatbotSessionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<ChatbotSession>> GetAllAsync(bool trackChanges = false)
        {
            return trackChanges
                ? await _dbContext.Set<ChatbotSession>()
                    .Include(s => s.Messages)
                    .ToListAsync()
                : await _dbContext.Set<ChatbotSession>()
                    .Include(s => s.Messages)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public Task<List<ChatbotSession>> GetAllActiveSessionsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
