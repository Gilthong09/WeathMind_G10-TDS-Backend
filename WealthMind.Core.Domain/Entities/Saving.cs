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
    public class Saving : AuditableBaseEntity
    {
        public string UserId { get; set; }

        [Required]
        public string AccountName { get; set; }

        public decimal Balance { get; set; } = 0;

        public decimal InterestRate { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
