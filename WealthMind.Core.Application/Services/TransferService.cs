using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransferService(IProductRepository productRepository, ITransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> TransferAsync(string fromProductId, string toProductId, decimal amount)
        {
            var fromProduct = await _productRepository.GetByIdWithTypeAsync(fromProductId);
            var toProduct = await _productRepository.GetByIdWithTypeAsync(toProductId);

            if (fromProduct == null || toProduct == null)
                throw new Exception("Uno de los productos no existe.");

            if (fromProduct.Balance < amount)
                throw new Exception("Saldo insuficiente.");

            // Validar la transferencia según los tipos de producto
            if (!IsValidTransfer(fromProduct, toProduct))
                throw new Exception("Transferencia no permitida entre estos tipos de productos.");

            fromProduct.Debit(amount);
            toProduct.Credit(amount);

            await _productRepository.UpdateAsync(fromProduct, fromProduct.Id);
            await _productRepository.UpdateAsync(toProduct, toProduct.Id);

            var transaction = new Transaction
            {
                FromProductId = fromProductId,
                ToProductId = toProductId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
            return true;
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
    }
}
