using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ITransferService
    {
        Task<bool> TransferAsync(SaveTransactionViewModel transaction);
        Task<bool> RegisterExpenseAsync(Product product, SaveTransactionViewModel transaction);
        Task<bool> RegisterIncomeAsync(Product product, SaveTransactionViewModel transaction);
    }
}
