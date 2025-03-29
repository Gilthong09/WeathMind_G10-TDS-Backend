using System.ComponentModel.DataAnnotations;

namespace WealthMind.Core.Application.ViewModels.ReportV
{
    public class SaveReportViewModel
    {
        public string UserId { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string ReportType { get; set; } // Monthly, Annual, etc.
        public string? StatisticsId { get; set; }
        public string? ProductId { get; set; }
        public string Summary { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
