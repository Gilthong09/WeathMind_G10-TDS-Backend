using System.ComponentModel.DataAnnotations;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{

    public class Recommendation : AuditableBaseEntity
    {
        public string UserId { get; set; }
        [Required]
        public string RecommendationText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string InsightType { get; set; } // Saving, Investing, etc.
        public string ReportId { get; set; }
        public Report? Report { get; set; } 

    }
}
