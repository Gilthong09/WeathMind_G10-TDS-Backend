using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Domain.Entities;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        private readonly ApplicationContext _dbContext;

        public ReportRepository(ApplicationContext dbContext) :base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<List<Report>> GetReportsByTypeAsync(string reportType)
        {
            throw new NotImplementedException();
        }

        public Task<List<Report>> GetReportsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
