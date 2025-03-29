using WealthMind.Core.Application.ViewModels.Product;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<SaveProductViewModel, ProductViewModel, Product>
    {
        Task<ProductViewModel> GetByIdWithTypeAsync(string id);
        ProductViewModel ConvertToViewModel(Product product);
        SaveProductViewModel ConvertToSaveViewModel(Product product);
    }
}
