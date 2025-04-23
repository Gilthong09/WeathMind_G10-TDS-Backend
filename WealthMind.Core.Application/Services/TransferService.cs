using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

public class TransferService : ITransferService
{
    private readonly IProductRepository _productRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransferService(IProductRepository productRepository, ITransactionRepository transactionRepository, IProductService productService, ITransactionService transactionService)
    {
        _productRepository = productRepository;
        _transactionRepository = transactionRepository;

    }

    public async Task<bool> TransferAsync(SaveTransactionViewModel transaction)
    {
        Product fromProduct, toProduct;

        if (transaction.FromProductId != null && transaction.ToProductId != null)
        {
            fromProduct = await _productRepository.GetByIdWithTypeAsync(transaction.FromProductId);
            toProduct = await _productRepository.GetByIdWithTypeAsync(transaction.ToProductId);

            if (!IsValidTransfer(fromProduct, toProduct))
                throw new Exception("Transferencia no permitida entre estos tipos de productos.");

            if (fromProduct.ProductType == "Loan" || fromProduct.ProductType == "CreditCard")
            {
                await AdjustSpecialProductValuesAsync(fromProduct, transaction.Amount, isIncome: false);
            }
            else
            {
                fromProduct.Debit(transaction.Amount);
            }

            if (toProduct.ProductType == "Loan" || toProduct.ProductType == "CreditCard")
            {
                await AdjustSpecialProductValuesAsync(toProduct, transaction.Amount, isIncome: true);
            }
            else
            {
                toProduct.Debit(transaction.Amount);
            }


            //await _productRepository.UpdateAsync(fromProduct, fromProduct.Id);
            //await _productRepository.UpdateAsync(toProduct, toProduct.Id);

            var transaction_1 = new Transaction
            {
                UserId = transaction.UserId,
                FromProductId = transaction.FromProductId,
                ToProductId = transaction.ToProductId,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description,
                Type = transaction.Type
                
            };

            await _transactionRepository.AddAsync(transaction_1);
            return true;
        }

        else if (transaction.FromProductId == null)
        {
            toProduct = await _productRepository.GetByIdWithTypeAsync(transaction.ToProductId);
            return await RegisterIncomeAsync(toProduct, transaction);
        }

        else if (transaction.ToProductId == null)
        {
            fromProduct = await _productRepository.GetByIdWithTypeAsync(transaction.FromProductId);
            return await RegisterExpenseAsync(fromProduct, transaction);
        }

        return false;
    }

    private bool IsValidTransfer(Product fromProduct, Product toProduct)
    {
        string fromType = fromProduct.GetType().Name;
        string toType = toProduct.GetType().Name;

        if (fromType == nameof(Saving) && toType == nameof(Loan)) return true;
        if (fromType == nameof(Cash) && toType == nameof(CreditCard)) return true;
        if (fromType == nameof(Cash) && toType == nameof(Saving)) return true;
        if (fromType == nameof(CreditCard) && toType == nameof(Loan)) return true;
        if (fromType == nameof(Saving) && toType == nameof(Cash)) return true;
        if (fromType == nameof(Saving) && toType == nameof(Investment)) return true;
        if (fromType == nameof(Saving) && toType == nameof(Saving)) return true;
        if (fromType == nameof(Cash) && toType == nameof(Loan)) return true;
        if (fromType == nameof(Cash) && toType == nameof(Investment)) return true;
        if (fromType == nameof(Saving) && toType == nameof(CreditCard)) return true;
        if (fromType == nameof(CreditCard) && toType == null) return true;
        if (fromType == nameof(Loan) && toType == null) return true;
        if (fromType == nameof(Cash) && toType == null) return true;
        if (fromType == nameof(Saving) && toType == null) return true;
        if (fromType == null && toType == nameof(CreditCard)) return true;
        if (fromType == null && toType == nameof(Loan)) return true;
        if (fromType == null && toType == nameof(Investment)) return true;
        if (fromType == null && toType == nameof(Saving)) return true;
        if (fromType == null && toType == nameof(Cash)) return true;
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
            TransactionDate = _transaction.TransactionDate,
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
            TransactionDate = _transaction.TransactionDate,
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
                        throw new InvalidOperationException("El monto excede la deuda del prestamo, usted Debe: " + loan.Debt);
                        
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
                        throw new InvalidOperationException("El monto excede el balance del prestamo, usted tiene disponible: " + loan.Balance);
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
                    if (creditCard.Debt < amount)
                    {

                        throw new InvalidOperationException("El monto excede la deuda de la tarjeta, usted Debe: " + creditCard.Debt);
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
                        throw new InvalidOperationException("El monto excede el balance de la tarjeta, usted tiene disponible: " + creditCard.Balance);
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
