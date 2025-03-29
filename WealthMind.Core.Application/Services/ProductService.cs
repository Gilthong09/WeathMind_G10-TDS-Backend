using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.Product;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class ProductService : GenericService<SaveProductViewModel, ProductViewModel, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper) : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> GetByIdWithTypeAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : ConvertToViewModel(product);
        }

        public async Task<SaveProductViewModel> Add(SaveProductViewModel vm)
        {
            Product product;

            switch (vm.ProductType)
            {
                case "Saving":
                    product = new Saving { Name = vm.Name, Balance = vm.Balance, UserId = vm.UserId};
                    break;
                case "Cash":
                    product = new Cash { Name = vm.Name, Balance = vm.Balance, UserId = vm.UserId };
                    break;
                case "CreditCard":
                    product = new CreditCard { Name = vm.Name, Balance = vm.Balance, CreditLimit = vm.CreditLimit, UserId = vm.UserId };
                    break;
                case "Loan":
                    product = new Loan { Name = vm.Name, Balance = vm.Balance, InterestRate = vm.InterestRate, TermInMonths = vm.TermInMonths, UserId = vm.UserId };
                    break;
                case "Investment":
                    product = new Investment { Name = vm.Name, Balance = vm.Balance, UserId = vm.UserId };
                    break;
                default:
                    throw new ArgumentException("Tipo de producto inválido");
            }

            product.Id = Guid.NewGuid().ToString();
            product.Created = DateTime.UtcNow;

            await _productRepository.AddAsync(product);
            return ConvertToSaveViewModel(product);
        }

        public async Task Update(SaveProductViewModel vm, string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new Exception("Producto no encontrado.");

            product.Name = vm.Name ?? product.Name;
            product.Balance = vm.Balance != default ? vm.Balance : product.Balance;

            if (product is CreditCard creditCard)
            {
                if (vm.CreditLimit.HasValue) creditCard.CreditLimit = vm.CreditLimit.Value;
            }
            else if (product is Loan loan)
            {
                if (vm.InterestRate.HasValue) loan.InterestRate = vm.InterestRate.Value;
            }

            await _productRepository.UpdateAsync(product, product.Id);
        }

        public async Task Delete(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<SaveProductViewModel> GetByIdSaveViewModel(string id, bool trackChanges = false)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new Exception("Producto no encontrado.");

            return ConvertToSaveViewModel(product);
        }

        public async Task<List<ProductViewModel>> GetAllViewModel(List<string> properties, bool trackChanges = false)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(ConvertToViewModel).ToList();
        }

        public async Task<List<ProductViewModel>> GetAllViewModel()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(ConvertToViewModel).ToList();
        }

        public ProductViewModel ConvertToViewModel(Product product)
        {
            var vm = _mapper.Map<ProductViewModel>(product);
            vm.ProductType = product switch
            {
                Saving => "Saving",
                Cash => "Cash",
                CreditCard => "CreditCard",
                Loan => "Loan",
                Investment => "Investment",
                _ => "Unknown"
            };
            return vm;
        }

        public SaveProductViewModel ConvertToSaveViewModel(Product product)
        {
            var vm = _mapper.Map<SaveProductViewModel>(product);
            vm.ProductType = product switch
            {
                Saving => "Saving",
                Cash => "Cash",
                CreditCard creditCard => "CreditCard",
                Loan loan => "Loan",
                Investment investment => "Investment",
                _ => throw new ArgumentException("Tipo de producto desconocido")
            };

            if (product is CreditCard creditCardProduct)
            {
                vm.CreditLimit = creditCardProduct.CreditLimit;
            }
            else if (product is Loan loanProduct)
            {
                vm.InterestRate = loanProduct.InterestRate;
                vm.Debt = loanProduct.Debt;
            }
      
            return vm;
        }
    }
}
