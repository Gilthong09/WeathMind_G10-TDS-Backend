using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.DTOs.Transactions
{
    public class AnnualStatistics
    {

        public List<MonthlyStatistics> MonthlyStatistics { get; set; } = new List<MonthlyStatistics>();
        public decimal TotalIncomeByYear { get; set; }
        public decimal TotalExpensesByYear { get; set; }
        public Dictionary<string, decimal> IncomePercentagesByMonth { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> ExpensePercentagesByMonth { get; set; } = new Dictionary<string, decimal>();
        public int NumberOfTransactions { get; set; }

    }
}
