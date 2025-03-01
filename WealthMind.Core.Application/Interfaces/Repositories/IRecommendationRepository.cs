using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IRecommendationRepository : IGenericRepository<Recommendation>
    {
        Task<List<Recommendation>> GetRecommendationsByUserIdAsync(string userId);
        Task<List<Recommendation>> GetRecommendationsByTypeAsync(string insightType);
    }
}
