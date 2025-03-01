using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IInvestmentRepository : IGenericRepository<Investment>
    {
        Task<List<Investment>> GetInvestmentsByUserIdAsync(string userId);
        Task<decimal> GetTotalInvestmentValueByUserIdAsync(string userId);
    }
}
