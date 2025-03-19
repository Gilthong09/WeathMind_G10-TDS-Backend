using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.TransferV
{
    public class SaveProductViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string ProductType { get; set; } // Saving, Cash, Loan, etc.
        public string? Currency { get; set; }
        public string? CardNumber { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? InterestRate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? InvestmentType { get; set; }
    }
}
