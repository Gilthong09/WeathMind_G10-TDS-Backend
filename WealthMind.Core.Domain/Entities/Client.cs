using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{

    public class Client
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public ICollection<Saving>? Savings { get; set; }
        public ICollection<Cash>? Beneficiaries { get; set; }
        public ICollection<CreditCard>? CreditCards { get; set; }
        public ICollection<Loan>? Loans { get; set; }
        public ICollection<Investment>? Investments { get; set; }

    }
    

}
