using System.ComponentModel.DataAnnotations;

namespace WealthMind.Core.Application.ViewModels.RecommendationV
{
    public class SaveRecommendationViewModel
    {
        public string UserId { get; set; }
        [Required]
        public string RecommendationText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? StatisticsId { get; set; }
        public string? ProductId { get; set; }
        public string InsightType { get; set; } // Saving, Investing, etc.
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
