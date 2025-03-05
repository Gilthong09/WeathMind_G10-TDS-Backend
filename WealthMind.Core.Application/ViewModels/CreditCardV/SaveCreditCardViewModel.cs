using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.CreditCard
{
    public class SaveCreditCardViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
