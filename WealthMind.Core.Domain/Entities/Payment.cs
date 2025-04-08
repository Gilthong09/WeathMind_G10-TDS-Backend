using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class Payment : AuditableBaseEntity
    {
        [Required]
        public string PaymentPlanId { get; set; } // ID del plan d pago

        [ForeignKey("PaymentPlanId")]
        public PaymentPlan PaymentPlan { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; } // Monto pagado en esta transacción

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow; // Fecha del pago

        public string Status { get; set; } = "Completed"; // "Pending", "Completed", "Failed"
    }
}
