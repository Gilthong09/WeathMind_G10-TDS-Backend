using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class Transaction : AuditableBaseEntity
    {

        public string UserId { get; set; }
        public string? FromProductId { get; set; }
        public string? ToProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public string Type { get; set; }
        public string? CategoryId { get; set; }

        public Product? FromProduct { get; set; }
        public Product? ToProduct { get; set; }
        public Category? Category { get; set; }
    }
}
