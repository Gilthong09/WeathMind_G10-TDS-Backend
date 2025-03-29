using System.ComponentModel.DataAnnotations;

namespace WealthMind.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        [Key]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
