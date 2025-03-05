using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Enums;

namespace WealthMind.Core.Application.ViewModels.CreditCard
{
    public class CreditCardViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal CreditLimit { get; set; }
        public ProductType Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }

}
