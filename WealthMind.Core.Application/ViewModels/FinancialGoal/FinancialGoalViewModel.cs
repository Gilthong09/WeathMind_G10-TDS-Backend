using WealthMind.Core.Application.ViewModels.Product;

namespace WealthMind.Core.Application.ViewModels.FinancialGoal
{
    public class FinancialGoalViewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime? TargetDate { get; set; }
        public ProductViewModel? Product { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
