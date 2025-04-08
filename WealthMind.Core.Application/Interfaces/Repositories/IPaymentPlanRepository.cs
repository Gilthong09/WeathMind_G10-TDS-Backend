using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IPaymentPlanRepository : IGenericRepository<PaymentPlan>
    {
        Task<List<PaymentPlan>> GetPaymentPlansByUserIdAsync(string userId);
        Task<List<PaymentPlan>> GetActivePaymentPlansByUserIdAsync(string userId);
        Task<List<PaymentPlan>> GetCompletedPaymentPlansByUserIdAsync(string userId);
        Task<List<Payment>> GetPaymentsByPlanIdAsync(string paymentPlanId);
    }
}
