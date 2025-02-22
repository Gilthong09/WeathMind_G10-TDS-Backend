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
    public class Investment: AuditableBaseEntity
    {

        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal InitialAmount { get; set; }

        public decimal? CurrentValue { get; set; }

        public decimal? ExpectedGrowth { get; set; }

        [Required]
        public DateTime InvestmentDate { get; set; }
    }
}
