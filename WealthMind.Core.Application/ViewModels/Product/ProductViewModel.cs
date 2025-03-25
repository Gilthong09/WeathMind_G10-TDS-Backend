using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string ProductType { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
