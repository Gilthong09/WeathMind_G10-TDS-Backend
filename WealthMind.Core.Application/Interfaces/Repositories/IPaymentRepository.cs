﻿using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<List<Payment>> GetPaymentsByUserIdAsync(string userId);
        Task<List<Payment>> GetPaymentsByPlanIdAsync(string paymentPlanId);
        Task<decimal> GetTotalPaidAmountByPlanIdAsync(string paymentPlanId);
        Task<int> GetPaidInstallmentsByPlanIdAsync(string paymentPlanId);
    }
}
