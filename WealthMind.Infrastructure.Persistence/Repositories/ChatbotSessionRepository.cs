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

        public async Task<List<ChatbotSession>> GetAllActiveSessionsByUserIdAsync(string userId)
        {
            return await _dbContext.Set<ChatbotSession>()
                .Include(s => s.Messages)
                .Where(s => s.UserId == userId && s.Status == "active")
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task UpdateAsync(ChatbotSession entity, string id)
        {
            var existingEntity = await _dbContext.Set<ChatbotSession>()
                .Include(s => s.Messages)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingEntity != null)
            {
                // Update only the modifiable properties
                existingEntity.ChatName = entity.ChatName;
                existingEntity.Status = entity.Status;
                existingEntity.LastModified = entity.LastModified;
                existingEntity.LastModifiedBy = entity.LastModifiedBy;

                _dbContext.Entry(existingEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
