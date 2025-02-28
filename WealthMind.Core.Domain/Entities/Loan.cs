using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Enums;

namespace WealthMind.Core.Domain.Entities
{
    public class Loan: Product
    {
        public decimal InterestRate { get; set; }
        public int TermInMonths { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal RemainingBalance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(TermInMonths);

        public Loan()
        {
            Type = ProductType.Loans;
        }
    }
}
