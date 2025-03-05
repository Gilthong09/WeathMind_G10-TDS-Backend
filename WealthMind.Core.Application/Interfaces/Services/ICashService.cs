using WealthMind.Core.Application.ViewModels.CashV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ICashService : IGenericService<SaveCashViewModel, CashViewModel, Cash>
    {

    }
}
