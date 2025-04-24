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

            if (fromProduct == null && toProduct == null)
            {
                throw new ArgumentException("No se puede procesar una transacción sin productos válidos.");
            }

            // Si `fromProduct` es `null`, significa que la transacción proviene de una cuenta externa
            if (fromProduct == null)
            {
                await RegisterIncomeAsync(toProduct, transaction);
                return true;
            }

            // Si `toProduct` es `null`, significa que la transacción va hacia una cuenta externa
            if (toProduct == null)
            {
                await RegisterExpenseAsync(fromProduct, transaction);
                return true;
            }

            // Manejo normal de la transacción
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


            /*if (fromProduct.ProductType == "Loan" || fromProduct.ProductType == "CreditCard")
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
            }*/


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

        /*else if (transaction.FromProductId == null)
        {
            toProduct = await _productRepository.GetByIdWithTypeAsync(transaction.ToProductId);
            return await RegisterIncomeAsync(toProduct, transaction);
        }

        else if (transaction.ToProductId == null)
        {
            fromProduct = await _productRepository.GetByIdWithTypeAsync(transaction.FromProductId);
            return await RegisterExpenseAsync(fromProduct, transaction);
        }*/

        return false;
    }

    private bool IsValidTransfer(Product fromProduct, Product toProduct)
    {
        // Si ambos son nulos, entonces es una transacción inválida
        if (fromProduct == null && toProduct == null)
        {
            throw new ArgumentException("No se puede procesar una transacción sin productos válidos.");
        }

        // Si uno de los productos es nulo, solo validamos el existente
        string fromType = fromProduct?.GetType().Name;
        string toType = toProduct?.GetType().Name;

        // Casos en los que solo hay destino (viene de una cuenta externa)
        if (fromType == null)
        {
            return toType == nameof(CreditCard) ||
                   toType == nameof(Loan) ||
                   toType == nameof(Investment) ||
                   toType == nameof(Saving) ||
                   toType == nameof(Cash);
        }

        // Casos en los que solo hay origen (va hacia una cuenta externa)
        if (toType == null)
        {
            return fromType == nameof(CreditCard) ||
                   fromType == nameof(Loan) ||
                   fromType == nameof(Cash) ||
                   fromType == nameof(Saving);
        }

        // Validación normal cuando ambos productos existen
        return (fromType == nameof(Saving) && toType == nameof(Loan)) ||
               (fromType == nameof(Cash) && toType == nameof(CreditCard)) ||
               (fromType == nameof(Cash) && toType == nameof(Saving)) ||
               (fromType == nameof(CreditCard) && toType == nameof(Loan)) ||
               (fromType == nameof(Saving) && toType == nameof(Cash)) ||
               (fromType == nameof(Saving) && toType == nameof(Investment)) ||
               (fromType == nameof(Saving) && toType == nameof(Saving)) ||
               (fromType == nameof(Cash) && toType == nameof(Loan)) ||
               (fromType == nameof(Cash) && toType == nameof(Investment)) ||
               (fromType == nameof(Saving) && toType == nameof(CreditCard));

        //string fromType = fromProduct.GetType().Name;
        //string toType = toProduct.GetType().Name;

        //if (fromType == nameof(Saving) && toType == nameof(Loan)) return true;
        //if (fromType == nameof(Cash) && toType == nameof(CreditCard)) return true;
        //if (fromType == nameof(Cash) && toType == nameof(Saving)) return true;
        //if (fromType == nameof(CreditCard) && toType == nameof(Loan)) return true;
        //if (fromType == nameof(Saving) && toType == nameof(Cash)) return true;
        //if (fromType == nameof(Saving) && toType == nameof(Investment)) return true;
        //if (fromType == nameof(Saving) && toType == nameof(Saving)) return true;
        //if (fromType == nameof(Cash) && toType == nameof(Loan)) return true;
        //if (fromType == nameof(Cash) && toType == nameof(Investment)) return true;
        //if (fromType == nameof(Saving) && toType == nameof(CreditCard)) return true;
        //if (fromType == nameof(CreditCard) && toType == null) return true;
        //if (fromType == nameof(Loan) && toType == null) return true;
        //if (fromType == nameof(Cash) && toType == null) return true;
        //if (fromType == nameof(Saving) && toType == null) return true;
        //if (fromType == null && toType == nameof(CreditCard)) return true;
        //if (fromType == null && toType == nameof(Loan)) return true;
        //if (fromType == null && toType == nameof(Investment)) return true;
        //if (fromType == null && toType == nameof(Saving)) return true;
        //if (fromType == null && toType == nameof(Cash)) return true;
        //return false;
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
            FromProductId = "478ade68-da71-4a72-a1d3-def708d95b09",
            ToProductId = _transaction.ToProductId,
            Amount = _transaction.Amount,
            CategoryId = _transaction.CategoryId,
            TransactionDate = _transaction.TransactionDate,
            Description = _transaction.Description,
            Type = _transaction.Type

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
            ToProductId = "478ade68-da71-4a72-a1d3-def708d95b09",
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
