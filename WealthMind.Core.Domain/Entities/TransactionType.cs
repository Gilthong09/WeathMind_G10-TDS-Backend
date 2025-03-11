using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Domain.Entities
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
