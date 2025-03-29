namespace WealthMind.Core.Application.ViewModels.FinancialGoal
{
    public class SaveFinancialGoalViewModel
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTime? TargetDate { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
   
}
