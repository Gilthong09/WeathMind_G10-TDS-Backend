using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.RecommendationV
{
    public class RecommendationViewModel
    {
        public string Id;
        public string UserId { get; set; }

        public string RecommendationText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? StatisticsId { get; set; }

        public string? ProductId { get; set; }

        public string InsightType { get; set; } // Saving, Investing, etc.

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
