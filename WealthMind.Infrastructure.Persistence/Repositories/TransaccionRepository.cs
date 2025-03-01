using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WealthMind.Infrastructure.Persistence.Repositories
{
    public class TransaccionRepository : GenericRepository<Transaction>
    {
        private readonly ApplicationContext _dbContext;

        public TransaccionRepository(ApplicationContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
