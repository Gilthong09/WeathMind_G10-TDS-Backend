using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class SavingRepository : GenericRepository<Saving>, ISavingRepository
    {
        private readonly ApplicationContext _dbContext;

        public SavingRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Saving>> GetSavingsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetTotalBalanceByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
