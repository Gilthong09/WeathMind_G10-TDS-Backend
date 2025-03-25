using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.FinancialGoal;
using WealthMind.Core.Domain.Entities;

public interface IFinancialGoalService : IGenericService<SaveFinancialGoalViewModel, FinancialGoalViewModel, FinancialGoal>
{

}
