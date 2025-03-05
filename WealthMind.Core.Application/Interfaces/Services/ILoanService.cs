using WealthMind.Core.Application.ViewModels.LoanV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanViewModel, LoanViewModel, Loan>
    {

    }
}
