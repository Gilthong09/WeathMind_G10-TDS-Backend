using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class Transaction: AuditableBaseEntity
    {

        public string UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("Category")]
        public string? CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; } //Depósito, Compra, Ganancia, Pago

        public DateTime TrxDate { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }

        public string Status { get; set; }


        [Required]
        public string EntitySourceType { get; set; } 

        [Required]
        public string EntitySourceId { get; set; }

        [Required]
        public string EntityDestinationType { get; set; }

        [Required]
        public string EntityDestinationId { get; set; }

    }
}
