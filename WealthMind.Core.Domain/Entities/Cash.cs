using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Enums;

namespace WealthMind.Core.Domain.Entities
{
    public class Cash : Product
    {
        public string Currency { get; set; } = "RD$";
        public Cash()
        {
            Type = ProductType.Cash;
        }
    }
}
