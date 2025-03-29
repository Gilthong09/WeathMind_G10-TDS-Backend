using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IRecommendationRepository : IGenericRepository<Recommendation>
    {
        Task<List<Recommendation>> GetRecommendationsByUserIdAsync(string userId);
        Task<List<Recommendation>> GetRecommendationsByTypeAsync(string insightType);
    }
}
