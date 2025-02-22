using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
