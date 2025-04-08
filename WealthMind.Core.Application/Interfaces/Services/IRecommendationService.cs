using WealthMind.Core.Application.ViewModels.RecommendationV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IRecommendationService : IGenericService<SaveRecommendationViewModel, RecommendationViewModel, Recommendation>
    {

    }
}
