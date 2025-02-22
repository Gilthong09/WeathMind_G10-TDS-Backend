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
    public class Recommendation: AuditableBaseEntity
    {

        public int UserId { get; set; }

        [Required]
        public string RecommendationText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string InsightType { get; set; } // Saving, Investing, etc.
    }
}
