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
        /// <summary>
        /// Obtiene todas las transacciones de un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Una lista de transacciones del usuario.</returns>
        Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId);

        /// <summary>
        /// Obtiene todas las transacciones de una categoría específica para un usuario.
        /// </summary>
        /// <param name="categoryId">ID de la categoría.</param>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Una lista de transacciones de la categoría seleccionada.</returns>
        Task<List<Transaction>> GetTransactionsByCategoryAsync(string categoryId, string userId);


        Task<decimal> GetTotalSpentByUserIdAsync(string userId);

        /// <summary>
        /// Calcula las estadísticas mensuales de ingresos y gastos por categoría para un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año para el cálculo.</param>
        /// <param name="month">Mes para el cálculo.</param>
        /// <returns>Un objeto <see cref="MonthlyStatistics"/> con información de ingresos, gastos y porcentajes por categoría.</returns>
        Task<MonthlyStatistics> SpendingPercentageByCategoryAsync(string userId, int year, int month);

        /// <summary>
        /// Calcula las estadísticas anuales de ingresos y gastos por categoría para un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año para el cálculo.</param>
        /// <returns>Un objeto <see cref="AnnualStatistics"/> con información de ingresos, gastos y porcentajes por mes.</returns>
        Task<AnnualStatistics> GetAnnualSpendingPercentageByCategoryAsync(string userId, int year);

        /// <summary>
        /// Obtiene todas las transacciones de un usuario en un rango de fechas.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="startDate">Fecha de inicio.</param>
        /// <param name="endDate">Fecha de fin.</param>
        /// <returns>Lista de transacciones en el rango de fechas.</returns>
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Calcula el total de ingresos de un usuario en un mes específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año del cálculo.</param>
        /// <param name="month">Mes del cálculo.</param>
        /// <returns>Total de ingresos en el mes.</returns>
        Task<decimal> GetTotalIncomeAsync(string userId, int year, int month);

        /// <summary>
        /// Calcula el total de gastos de un usuario en un mes específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año del cálculo.</param>
        /// <param name="month">Mes del cálculo.</param>
        /// <returns>Total de gastos en el mes.</returns>
        Task<decimal> GetTotalExpensesAsync(string userId, int year, int month);

        /// <summary>
        /// Obtiene las principales transacciones de gasto de un usuario en un mes específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año del cálculo.</param>
        /// <param name="month">Mes del cálculo.</param>
        /// <param name="topN">Número de transacciones principales a obtener.</param>
        /// <returns>Lista de las principales transacciones de gasto.</returns>
        Task<List<Transaction>> GetTopExpensesByCategoryAsync(string userId, int year, int month, int topN);
    }
}
