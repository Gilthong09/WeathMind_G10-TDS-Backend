namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ITransferService
    {
        Task<bool> TransferAsync(string fromProductId, string toProductId, decimal amount);
    }
}
