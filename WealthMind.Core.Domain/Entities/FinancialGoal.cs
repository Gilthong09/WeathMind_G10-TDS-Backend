using System.ComponentModel.DataAnnotations;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class FinancialGoal : AuditableBaseEntity
    {
        public string UserId { get; set; }

        public string ProductId { get; set; } //FK

        public string Name { get; set; }

        [Required]
        public decimal TargetAmount { get; set; }

        public decimal CurrentAmount { get; set; } = 0;

        public DateTime? TargetDate { get; set; }

        // public string Status { get; set; } = "active";

        public Product? Product { get; set; }
    }
}
