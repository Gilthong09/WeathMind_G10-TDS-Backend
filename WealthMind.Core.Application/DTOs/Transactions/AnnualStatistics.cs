using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.DTOs.Transactions
{
    public class AnnualStatistics
    {
        public List<MonthlyStatistics> MonthlyStatistics { get; set; }
        public Dictionary<string, decimal> PercentagesMonths { get; set; }
        public int NumberOfTransaction { get; set; }
        public int TotalMoneyByYear { get; set; }

    }
}
