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
    public class RecommendationRepository: GenericRepository<Recommendation>, IRecommendationRepository
    {
        private readonly ApplicationContext _dbContext;

        public RecommendationRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<List<Recommendation>> GetRecommendationsByTypeAsync(string insightType)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recommendation>> GetRecommendationsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
