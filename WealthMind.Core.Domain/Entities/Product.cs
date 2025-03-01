using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Enums;

namespace WealthMind.Core.Domain.Entities
{
    public abstract class Product: AuditableBaseEntity
    {
        public string UserId { get; set; }

        [Required]
        public ProductType Type { get; set; } 

        [Required]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
