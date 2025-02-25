using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<List<Report>> GetReportsByUserIdAsync(string userId);
        Task<List<Report>> GetReportsByTypeAsync(string reportType);
    }
}
