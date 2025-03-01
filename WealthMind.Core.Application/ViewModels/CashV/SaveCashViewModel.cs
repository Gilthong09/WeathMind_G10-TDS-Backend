using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.CashV
{
    public class SaveCashViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
