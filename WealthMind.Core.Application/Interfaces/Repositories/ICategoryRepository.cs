using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoriesByTypeAsync(string type);
    }
}
