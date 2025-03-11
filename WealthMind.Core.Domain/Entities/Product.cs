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
        public ProductType ProductType { get; set; } 

        [Required]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        
    }
}
