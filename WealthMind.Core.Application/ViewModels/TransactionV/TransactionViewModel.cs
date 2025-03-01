using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Enums;

namespace WealthMind.Core.Application.ViewModels.TransactionV
{
    public class TransactionViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public string TransactionType { get; set; }
        public DateTime TrxDate { get; set; }
        public ProductType Type { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EntitySourceType { get; set; }
        public string EntitySourceId { get; set; }
        public string EntityDestinationType { get; set; }
        public string EntityDestinationId { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
