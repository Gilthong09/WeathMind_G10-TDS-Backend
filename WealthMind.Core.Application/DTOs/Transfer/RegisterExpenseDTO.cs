namespace WealthMind.Core.Application.DTOs.Transfer
{
    public class RegisterExpenseDto
    {
        public string FromProductId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
