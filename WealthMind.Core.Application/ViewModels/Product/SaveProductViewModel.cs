namespace WealthMind.Core.Application.ViewModels.Product
{
    public class SaveProductViewModel
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string? ProductType { get; set; } // Saving, Cash, Loan, etc.
        public decimal? CreditLimit { get; set; }
        public decimal? Debt { get; set; }
        public int TermInMonths { get; set; } = 0;
        public decimal? InterestRate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }

}
