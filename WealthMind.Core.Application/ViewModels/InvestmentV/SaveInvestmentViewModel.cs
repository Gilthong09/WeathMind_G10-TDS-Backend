using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.InvestmentV
{
    public class SaveInvestmentViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal ExpectedReturn { get; set; }
        public int DurationInMonths { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
