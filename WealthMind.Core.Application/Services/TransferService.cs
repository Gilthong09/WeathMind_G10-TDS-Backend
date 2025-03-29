using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

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
        Product fromProduct, toProduct;

        if (_transaction.FromProductId != null && _transaction.ToProductId != null)
        {
            fromProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.FromProductId);
            toProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.ToProductId);

            if (!IsValidTransfer(fromProduct, toProduct))
                throw new Exception("Transferencia no permitida entre estos tipos de productos.");

            if(fromProduct.ProductType == "Loan" || fromProduct.ProductType == "CreditCard")
            {
                await AdjustSpecialProductValuesAsync(fromProduct, _transaction.Amount, isIncome: false);
            }
            else
            {
                fromProduct.Debit(_transaction.Amount);
            }

            if (toProduct.ProductType == "Loan" || toProduct.ProductType == "CreditCard")
            {
                await AdjustSpecialProductValuesAsync(toProduct, _transaction.Amount, isIncome: true);
            }
            else
            {
                toProduct.Debit(_transaction.Amount);
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

        else if (_transaction.FromProductId == null)
        {
            toProduct = await _productRepository.GetByIdWithTypeAsync(_transaction.ToProductId);
            return await RegisterIncomeAsync(toProduct, _transaction);
        }

        else if (_transaction.ToProductId == null)
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

        if (fromType == nameof(Saving) && toType == nameof(Loan)) return true;
        if (fromType == nameof(Cash) && toType == nameof(CreditCard)) return true;
        if (fromType == nameof(CreditCard) && toType == nameof(Loan)) return true;
        if (fromType == nameof(Saving) && toType == nameof(Cash)) return true;
        if (fromType == nameof(Saving) && toType == nameof(Investment)) return true;
        if (fromType == nameof(Saving) && toType == nameof(Saving)) return true;
        if (fromType == nameof(Cash) && toType == nameof(Loan)) return true;

        return false;
    }

    public async Task<bool> RegisterIncomeAsync(Product toProduct, SaveTransactionViewModel _transaction)
    {
        if (toProduct == null) throw new Exception("Cuenta destino no encontrada.");
        toProduct.Credit(_transaction.Amount);

        await AdjustSpecialProductValuesAsync(toProduct, _transaction.Amount, isIncome: true);

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
        fromProduct.Debit(_transaction.Amount);

        await AdjustSpecialProductValuesAsync(fromProduct, _transaction.Amount, isIncome: false);

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

    private async Task AdjustSpecialProductValuesAsync(Product product, decimal amount, bool isIncome)
    {
        switch (product)
        {
            case Loan loan:
                if (isIncome)
                {
                    if (loan.Debt < amount)
                    {
                        break;
                    }
                    else 
                    { 
                        
                        loan.Debt -= amount;
                    
                    }
                }
                else
                {
                    if (loan.Balance < amount)
                    {
                        break;
                    }
                    else
                    {
                        loan.Balance -= amount;
                    }
                   
                }
                break;

            case CreditCard creditCard:
                if (isIncome)
                {
                    if(creditCard.Debt < amount)
                    {
                       
                        break;
                    }
                    else
                    {
                        creditCard.Debt -= amount;
                    }
                }
                else
                {
                    if (creditCard.Balance < amount)
                    {
                        break;
                    }
                    else
                    {
                        creditCard.Balance -= amount;
                        creditCard.Debt += amount;
                    }
                    
                }
                break;

        }

        await _productRepository.UpdateAsync(product, product.Id);
    }
}
