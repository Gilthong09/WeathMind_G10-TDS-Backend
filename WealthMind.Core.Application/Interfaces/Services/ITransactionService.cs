using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Application.DTOs.Transactions;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    /// <summary>
    /// Define las operaciones para la gestión de transacciones.
    /// </summary>
    public interface ITransactionService : IGenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>
    {
        /// <summary>
        /// Obtiene todas las transacciones de un usuario.
        /// </summary>
        Task<List<TransactionViewModel>> GetTransactionsByUserIdAsync(string userId);

        /// <summary>
        /// Obtiene todas las transacciones de una categoría específica para un usuario.
        /// </summary>
        Task<List<TransactionViewModel>> GetTransactionsByCategoryAsync(string categoryId, string userId);

        /// <summary>
        /// Obtiene todas las transacciones en un rango de fechas.
        /// </summary>
        Task<List<TransactionViewModel>> GetTransactionsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Calcula el total de ingresos de un usuario en un mes específico.
        /// </summary>
        Task<decimal> GetTotalIncomeAsync(string userId, int year, int month);

        /// <summary>
        /// Calcula el total de gastos de un usuario en un mes específico.
        /// </summary>
        Task<decimal> GetTotalExpensesAsync(string userId, int year, int month);

        /// <summary>
        /// Obtiene las principales transacciones de gasto en un mes.
        /// </summary>
        Task<List<TransactionViewModel>> GetTopExpensesByCategoryAsync(string userId, int year, int month, int topN);

        /// <summary>
        /// Calcula las estadísticas mensuales de ingresos y gastos.
        /// </summary>
        Task<MonthlyStatistics> SpendingPercentageByCategoryAsync(string userId, int year, int month);

        /// <summary>
        /// Calcula las estadísticas anuales de ingresos y gastos.
        /// </summary>
        Task<AnnualStatistics> GetAnnualSpendingPercentageByCategoryAsync(string userId, int year);

    }
}
