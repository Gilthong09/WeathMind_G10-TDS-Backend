using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class FinancialGoalRepository : GenericRepository<FinancialGoal>, IFinancialGoalRepository
    {
        private readonly ApplicationContext _dbContext;

        public FinancialGoalRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
