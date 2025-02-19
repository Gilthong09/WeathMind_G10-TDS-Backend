using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Domain.Entities
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string RecommendationText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string InsightType { get; set; } // Saving, Investing, etc.
    }
}
