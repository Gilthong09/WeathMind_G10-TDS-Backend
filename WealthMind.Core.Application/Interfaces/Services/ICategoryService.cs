using WealthMind.Core.Application.ViewModels.CategoryV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<SaveCategoryViewModel, CategoryViewModel, Category>
    {

    }
}
