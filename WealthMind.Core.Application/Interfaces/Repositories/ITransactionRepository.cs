using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Application.DTOs.Transactions;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        /*Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId);
        Task<List<Transaction>> GetTransactionsByCategoryAsync(string categoryId, string userId);
        Task<decimal> GetTotalSpentByUserIdAsync(string userId);

        //Reportes y graficos
        Task<MonthlyStatistics> SpendingPercentageByCategoryAsync(string userId, int year, int month);
        Task<AnnualStatistics> GetAnnualSpendingPercentageByCategoryAsync(string userId, int year);

        //Otros
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalIncomeAsync(string userId, int year, int month);
        Task<decimal> GetTotalExpensesAsync(string userId, int year, int month);
        Task<List<Transaction>> GetTopExpensesByCategoryAsync(string userId, int year, int month, int topN);*/
    }
}
