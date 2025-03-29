using System.ComponentModel.DataAnnotations;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class PaymentPlan : AuditableBaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RelatedEntityId { get; set; } // Puede ser una Meta, Inversión o Ahorro

        [Required]
        public string EntityType { get; set; } // "FinancialGoal", "Investment", "Saving"

        [Required]
        public decimal TotalAmount { get; set; } // Monto total a pagar

        [Required]
        public int TotalInstallments { get; set; } // Cantidad total de plazos

        public int PaidInstallments { get; set; } = 0; // Cantidad de plazos pagados

        public decimal PaidAmount { get; set; } = 0; // Monto total pagado

        public DateTime StartDate { get; set; } = DateTime.UtcNow; // Fecha de inicio

        public DateTime? EndDate { get; set; } // Fecha de finalización si ya está pagado completamente

        // Relación con los pagos individuales
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
