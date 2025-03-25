using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public AuditableBaseEntity()
        { 

        }

        [Key]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
