namespace WealthMind.Core.Application.DTOs.Transfer
{
    public class RegisterIncomeDto
    {
        public string ToProductId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
