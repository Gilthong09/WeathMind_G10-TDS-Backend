using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ITransferService
    {
        Task<SaveTransactionViewModel> Transfer();
        Task<bool> AddMoneyToAccount();
    }
}
