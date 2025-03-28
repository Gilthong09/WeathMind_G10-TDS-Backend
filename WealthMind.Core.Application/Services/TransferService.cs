using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.Product;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductService _productServcie;
        private readonly ITransactionService _transactionService;

        public TransferService(IProductRepository productRepository, ITransactionRepository transactionRepository, IProductService productService, ITransactionService transactionService)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _productServcie = productService;
            _transactionService = transactionService;
        }

        public async Task<bool> TransferAsync(SaveTransactionViewModel _transaction)
        {
            Product fromProduct,toProduct;

            if (_transaction.FromProductId != null && _transaction.ToProductId != null)
            {
                fromProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.FromProductId);
                toProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.ToProductId);
                // Validar la transferencia según los tipos de producto
                if (!IsValidTransfer(fromProduct, toProduct))
                    throw new Exception("Transferencia no permitida entre estos tipos de productos.");

                fromProduct.Debit(_transaction.Amount);
                toProduct.Credit(_transaction.Amount);
                if(toProduct.ProductType == "Loan")
                {
                    SaveProductViewModel svp = _productServcie.ConvertToSaveViewModel(toProduct);
                    svp.Debt -= _transaction.Amount;

                   await _productServcie.Update(svp, svp.Id);
                }

                await _productRepository.UpdateAsync(fromProduct, fromProduct.Id);
                await _productRepository.UpdateAsync(toProduct, toProduct.Id);

                var transaction = new Transaction
                {
                    UserId = _transaction.UserId,
                    FromProductId = _transaction.FromProductId,
                    ToProductId = _transaction.ToProductId,
                    Amount = _transaction.Amount,
                    CategoryId = _transaction.CategoryId,
                    TransactionDate = DateTime.UtcNow,
                    Description = _transaction.Description
                };

                await _transactionRepository.AddAsync(transaction);
                return true;
            }

            else if(_transaction.FromProductId == null)
            {
                toProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.ToProductId);
                return await RegisterIncomeAsync(toProduct, _transaction);

            }
            else if(_transaction.ToProductId == null)
            {
                fromProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.FromProductId);
                return await RegisterExpenseAsync(fromProduct, _transaction);
            }

            return false;
            
        }

        private bool IsValidTransfer(Product fromProduct, Product toProduct)
        {
            string fromType = fromProduct.GetType().Name;
            string toType = toProduct.GetType().Name;

            // Reglas de transferencia permitidas
            if (fromType == nameof(Saving) && toType == nameof(Loan))
                return true; // Pagar parte de un préstamo con una cuenta de ahorro

            if (fromType == nameof(Cash) && toType == nameof(CreditCard))
                return true; // Pagar una tarjeta de crédito con efectivo

            if (fromType == nameof(CreditCard) && toType == nameof(Loan))
                return true; // Pagar un préstamo con tarjeta de crédito

            if (fromType == nameof(Saving) && toType == nameof(Cash))
                return true; // Retiro de ahorro a efectivo

            if (fromType == nameof(Saving) && toType == nameof(Investment))
                return true; // Invertir desde cuenta de ahorro

            return false; // Bloquear cualquier otra combinación
        }
        public async Task<bool> RegisterIncomeAsync(Product toProduct, SaveTransactionViewModel _transaction)
        {
            if (toProduct == null) throw new Exception("Cuenta origen no encontrada.");
            toProduct.Credit(_transaction.Amount);

                await _productRepository.UpdateAsync(toProduct, toProduct.Id);

                var transaction = new Transaction
                {
                    UserId = _transaction.UserId,
                    FromProductId = ProductTypes.Other.ToString(),
                    ToProductId = _transaction.ToProductId,
                    Amount = _transaction.Amount,
                    CategoryId = _transaction.CategoryId,
                    TransactionDate = DateTime.UtcNow,
                    Description = _transaction.Description
                };

                await _transactionRepository.AddAsync(transaction);
                return true;
        }

        public async Task<bool> RegisterExpenseAsync(Product fromProduct, SaveTransactionViewModel _transaction)
        {
            if (fromProduct == null) throw new Exception("Cuenta origen no encontrada.");
            fromProduct.Credit(_transaction.Amount);

            await _productRepository.UpdateAsync(fromProduct, fromProduct.Id);

            var transaction = new Transaction
            {
                UserId = _transaction.UserId,
                FromProductId = _transaction.FromProductId,
                ToProductId = ProductTypes.Other.ToString(),
                Amount = _transaction.Amount,
                CategoryId = _transaction.CategoryId,
                TransactionDate = DateTime.UtcNow,
                Description = _transaction.Description
            };

            await _transactionRepository.AddAsync(transaction);
            return true;
        }

       

    }
}
