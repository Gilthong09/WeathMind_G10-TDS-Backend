using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Infrastructure.Persistence.Contexts;
using WealthMind.Infrastructure.Persistence.Repositories;
using WealthMind.Infrastructure.Persistence.Repository;

namespace WealthMind.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region "Context Configurations"

            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();     
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IRecommendationRepository, RecommendationRepository>();
            /*services.AddTransient<IPropertyAsync, PropertyRepository>();
            services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            services.AddTransient<IPropertyAmenityAsync, PropertyAmenityRepository>();
            services.AddTransient<ITypePropertyAsync, TypePropertyRepository>();
            services.AddTransient<ITypeSaleAsync, TypeSaleRepository>();*/
            #endregion
        }
    }
}
