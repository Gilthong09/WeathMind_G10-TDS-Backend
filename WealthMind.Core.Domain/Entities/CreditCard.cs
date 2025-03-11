using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Enums;

namespace WealthMind.Core.Domain.Entities
{
    public class CreditCard: Product
    {
        public DateTime ExpirationDate { get; set; }
        public decimal CreditLimit { get; set; }

       

    }
}
