using Microsoft.EntityFrameworkCore;
using WealthMind.Core.Application.DTOs.Transactions;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transaction = WealthMind.Core.Domain.Entities.Transaction;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationContext _dbContext;

        public TransactionRepository(ApplicationContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene todas las transacciones de un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Una lista de transacciones del usuario.</returns>
        public async Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todas las transacciones de una categoría específica para un usuario.
        /// </summary>
        /// <param name="categoryId">ID de la categoría.</param>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Una lista de transacciones de la categoría seleccionada.</returns>
        public async Task<List<Transaction>> GetTransactionsByCategoryAsync(string categoryId, string userId)
        {
            return await _dbContext.Transactions
                .Where(t => t.CategoryId == categoryId && t.UserId == userId)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalSpentByUserIdAsync(string userId)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId && t.Amount < 0)
                .SumAsync(t => t.Amount);
        }

        /// <summary>
        /// Calcula las estadísticas mensuales de ingresos y gastos por categoría para un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año para el cálculo.</param>
        /// <param name="month">Mes para el cálculo.</param>
        /// <returns>Un objeto <see cref="MonthlyStatistics"/> con información de ingresos, gastos y porcentajes por categoría.</returns>

        public async Task<MonthlyStatistics> SpendingPercentageByCategoryAsync(string userId, int year, int month)
        {
            var transactions = await _dbContext.Transactions
                .Where(t => t.UserId == userId && t.TransactionDate.Year == year && t.TransactionDate.Month == month)
                .ToListAsync();

            var totalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            var totalExpenses = transactions.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount));

            var categoriesWithTransactions = transactions.Select(t => t.Category).Distinct().ToList();

            var incomePercentageByCategory = transactions
                .Where(t => t.Amount > 0)
                .GroupBy(t => t.Category.Name)
                .ToDictionary(g => g.Key, g => (g.Sum(t => t.Amount) / totalIncome) * 100);

            var expensePercentageByCategory = transactions
                .Where(t => t.Amount < 0)
                .GroupBy(t => t.Category.Name)
                .ToDictionary(g => g.Key, g => (Math.Abs(g.Sum(t => t.Amount)) / totalExpenses) * 100);

            return new MonthlyStatistics
            {
                CategoriesWithTransactions = categoriesWithTransactions,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                IncomePercentageByCategory = incomePercentageByCategory,
                ExpensePercentageByCategory = expensePercentageByCategory,
                NumberOfTransactions = transactions.Count
            };
        }

        /// <summary>
        /// Calcula las estadísticas anuales de ingresos y gastos por categoría para un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año para el cálculo.</param>
        /// <returns>Un objeto <see cref="AnnualStatistics"/> con información de ingresos, gastos y porcentajes por mes.</returns>
        public async Task<AnnualStatistics> GetAnnualSpendingPercentageByCategoryAsync(string userId, int year)
        {
            var annualStatistics = new AnnualStatistics
            {
                MonthlyStatistics = new List<MonthlyStatistics>(),
                IncomePercentagesByMonth = new Dictionary<string, decimal>(),
                ExpensePercentagesByMonth = new Dictionary<string, decimal>()
            };

            decimal totalYearlyIncome = 0;
            decimal totalYearlyExpenses = 0;

            for (int month = 1; month <= 12; month++)
            {
                var monthlyStats = await SpendingPercentageByCategoryAsync(userId, year, month);
                annualStatistics.MonthlyStatistics.Add(monthlyStats);
                totalYearlyIncome += monthlyStats.TotalIncome;
                totalYearlyExpenses += monthlyStats.TotalExpenses;
            }

            for (int month = 1; month <= 12; month++)
            {
                var monthlyStats = annualStatistics.MonthlyStatistics[month - 1];

                annualStatistics.IncomePercentagesByMonth[month.ToString()] =
                    totalYearlyIncome > 0 ? (monthlyStats.TotalIncome / totalYearlyIncome) * 100 : 0;

                annualStatistics.ExpensePercentagesByMonth[month.ToString()] =
                    totalYearlyExpenses > 0 ? (monthlyStats.TotalExpenses / totalYearlyExpenses) * 100 : 0;
            }

            annualStatistics.TotalIncomeByYear = totalYearlyIncome;
            annualStatistics.TotalExpensesByYear = totalYearlyExpenses;
            annualStatistics.NumberOfTransactions = annualStatistics.MonthlyStatistics.Sum(m => m.NumberOfTransactions);

            return annualStatistics;
        }

        /// <summary>
        /// Obtiene todas las transacciones de un usuario en un rango de fechas.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="startDate">Fecha de inicio.</param>
        /// <param name="endDate">Fecha de fin.</param>
        /// <returns>Lista de transacciones en el rango de fechas.</returns>
        public async Task<List<Transaction>> GetTransactionsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId && t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Calcula el total de ingresos de un usuario en un mes específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año del cálculo.</param>
        /// <param name="month">Mes del cálculo.</param>
        /// <returns>Total de ingresos en el mes.</returns>
        public async Task<decimal> GetTotalIncomeAsync(string userId, int year, int month)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId && t.TransactionDate.Year == year && t.TransactionDate.Month == month && t.Amount > 0)
                .SumAsync(t => t.Amount);
        }

        /// <summary>
        /// Calcula el total de gastos de un usuario en un mes específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año del cálculo.</param>
        /// <param name="month">Mes del cálculo.</param>
        /// <returns>Total de gastos en el mes.</returns>
        public async Task<decimal> GetTotalExpensesAsync(string userId, int year, int month)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId && t.TransactionDate.Year == year && t.TransactionDate.Month == month && t.Amount < 0)
                .SumAsync(t => t.Amount);
        }

        /// <summary>
        /// Obtiene las principales transacciones de gasto de un usuario en un mes específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="year">Año del cálculo.</param>
        /// <param name="month">Mes del cálculo.</param>
        /// <param name="topN">Número de transacciones principales a obtener.</param>
        /// <returns>Lista de las principales transacciones de gasto.</returns>
        public async Task<List<Transaction>> GetTopExpensesByCategoryAsync(string userId, int year, int month, int topN)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId && t.TransactionDate.Year == year && t.TransactionDate.Month == month && t.Amount < 0)
                .OrderBy(t => t.Amount)
                .Take(topN)
                .ToListAsync();
        }
    }
}
