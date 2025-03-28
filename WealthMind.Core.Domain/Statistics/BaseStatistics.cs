using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Domain.Statistics
{
    public abstract class BaseStatistics: AuditableBaseEntity
    {
        public string TypeStatistic { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public int NumberOfTransactions { get; set; }
        public List<Report>? Report { get; set; }
        public List<Recommendation>? Recommendation { get; set; }
    }
}
