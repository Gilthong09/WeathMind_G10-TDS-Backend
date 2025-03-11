using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.TransactionV
{
    public class SaveTransactionViewModel
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string CategoryId { get; set; }
        public int? TransactionTypeId { get; set; }
        public DateTime TrxDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EntitySourceType { get; set; }
        public string EntitySourceId { get; set; }
        public string EntityDestinationType { get; set; }
        public string EntityDestinationId { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
