namespace WealthMind.Core.Domain.Statistics
{
    public class AnnualStatistics : BaseStatistics
    {
        public List<MonthlyStatistics> MonthlyStatistics { get; set; } = new List<MonthlyStatistics>();
        public Dictionary<string, decimal> IncomePercentagesByMonth { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> ExpensePercentagesByMonth { get; set; } = new Dictionary<string, decimal>();
    }
}
