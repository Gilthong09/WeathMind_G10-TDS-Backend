using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IFinancialGoalRepository : IGenericRepository<FinancialGoal>
    {
        Task<List<FinancialGoal>> GetActiveGoalsByUserIdAsync(string userId);
        Task<List<FinancialGoal>> GetCompletedGoalsByUserIdAsync(string userId);
    }
}
