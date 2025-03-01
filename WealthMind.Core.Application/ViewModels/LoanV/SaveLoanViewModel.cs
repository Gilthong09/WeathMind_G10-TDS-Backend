using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.LoanV
{
    public class SaveLoanViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal InterestRate { get; set; }
        public int TermInMonths { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal RemainingBalance { get; set; }
        public DateTime StartDate { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
