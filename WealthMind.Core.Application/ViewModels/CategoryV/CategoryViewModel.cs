using System.ComponentModel.DataAnnotations;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.ViewModels.CategoryV
{
    public class CategoryViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public string UserId { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
