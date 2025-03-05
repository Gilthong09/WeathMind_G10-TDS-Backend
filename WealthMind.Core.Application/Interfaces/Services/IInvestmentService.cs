using WealthMind.Core.Application.ViewModels.InvestmentV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IInvestmentService : IGenericService<SaveInvestmentViewModel, InvestmentViewModel, Investment>
    {

    }
}
