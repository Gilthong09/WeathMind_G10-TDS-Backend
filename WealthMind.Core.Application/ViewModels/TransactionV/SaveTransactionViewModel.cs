using WealthMind.Core.Application.ViewModels.Product;

namespace WealthMind.Core.Application.ViewModels.TransactionV
{
    public class SaveTransactionViewModel
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string? CategoryId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Description { get; set; }
        public ProductViewModel? FromProduct { get; set; }
        public string? FromProductId { get; set; }
        public string? ToProductId { get; set; }
        public ProductViewModel? ToProduct { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
