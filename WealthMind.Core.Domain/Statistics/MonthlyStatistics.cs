using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Domain.Statistics
{
    public class MonthlyStatistics : BaseStatistics
    {
        public List<Category> CategoriesWithTransactions { get; set; } = new List<Category>();
        public Dictionary<string, decimal> IncomePercentageByCategory { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> ExpensePercentageByCategory { get; set; } = new Dictionary<string, decimal>();

    }
}
