namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ITransferService
    {
        Task<bool> TransferAsync(string fromProductId, string toProductId, decimal amount);
        Task<bool> RegisterExpenseAsync(string fromProductId, decimal amount, string description);
        Task<bool> RegisterIncomeAsync(string toProductId, decimal amount, string description);
    }
}
