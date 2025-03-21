using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.DTOs.Transfer
{
    public class RegisterExpenseDto
    {
        public string FromProductId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
