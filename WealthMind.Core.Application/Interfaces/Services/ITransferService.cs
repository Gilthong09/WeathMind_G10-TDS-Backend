namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ITransferService
    {
        Task<bool> TransferAsync(int fromProductId, int toProductId, decimal amount);
    }
}
