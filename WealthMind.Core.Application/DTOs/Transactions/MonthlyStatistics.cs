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
        public List<Category> CategoriesWithTransactions { get; set; }
        public Dictionary<string, decimal> PercentageByCategories { get; set; }
        public int NumberOfTransaction { get; set; }
        public int TotalMoneyByMonth { get; set; }

    }
}
