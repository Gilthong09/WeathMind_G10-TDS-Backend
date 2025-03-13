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
        public int? FromProductId { get; set; }
        public int? ToProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public string? CategoryId { get; set; }

        public Product? FromProduct { get; set; }
        public Product? ToProduct { get; set; }
        public Category? Category { get; set; }
    }
}
