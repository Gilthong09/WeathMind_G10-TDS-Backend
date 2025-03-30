using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class CategoryType: AuditableBaseEntity
    {
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
