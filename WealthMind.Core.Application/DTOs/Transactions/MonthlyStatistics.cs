using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.DTOs.Transactions
{
    public class MonthlyStatistics
    {
        public List<Category> CategoriesWithTransactions { get; set; } = new List<Category>();
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public Dictionary<string, decimal> IncomePercentageByCategory { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> ExpensePercentageByCategory { get; set; } = new Dictionary<string, decimal>();
        public int NumberOfTransactions { get; set; }

        public List<Report>? Report { get; set; }
        public List<Recommendation>? Recommendation { get; set; }

    }
}
