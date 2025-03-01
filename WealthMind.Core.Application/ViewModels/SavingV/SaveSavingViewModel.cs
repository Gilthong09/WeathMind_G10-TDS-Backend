using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthMind.Core.Application.ViewModels.SavingV
{
    public class SaveSavingViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
