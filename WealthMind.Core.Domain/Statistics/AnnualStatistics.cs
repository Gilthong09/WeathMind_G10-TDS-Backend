using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Domain.Statistics
{
    public class AnnualStatistics: BaseStatistics
    {
        public List<MonthlyStatistics> MonthlyStatistics { get; set; } = new List<MonthlyStatistics>();
        public Dictionary<string, decimal> IncomePercentagesByMonth { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> ExpensePercentagesByMonth { get; set; } = new Dictionary<string, decimal>();
    }
}
