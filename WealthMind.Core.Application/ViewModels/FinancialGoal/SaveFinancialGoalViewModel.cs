namespace WealthMind.Core.Application.ViewModels.FinancialGoal
{
    public class SaveFinancialGoalViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string? ProductType { get; set; } // Saving, Cash, Loan, etc.
        public decimal? CreditLimit { get; set; }
        public int TermInMonths { get; set; } = 0;
        public decimal? InterestRate { get; set; }
        public DateTime? EndDate { get; set; }
    }
   
}
