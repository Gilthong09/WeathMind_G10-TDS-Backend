﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public class Report : AuditableBaseEntity
    {
        public string UserId { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string ReportType { get; set; } // Monthly, Annual, etc.
        public string? StatisticsId { get; set; }
        public string? ProductId { get; set; }
        public string Summary { get; set; }

        //public Recommendation
    }
}
