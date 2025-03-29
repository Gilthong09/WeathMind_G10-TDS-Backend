using System.ComponentModel.DataAnnotations;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {

        [Required]
        public string Name { get; set; }// Salario, Alimentación, Inversiones, Transporte
        [Required]
        public string Type { get; set; }// Ingreso, Gasto

        public string UserId { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
