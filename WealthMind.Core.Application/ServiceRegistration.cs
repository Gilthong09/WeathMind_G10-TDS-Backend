using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services;
using WealthMind.Core.Application.Services.MainServices;

namespace WealthMind.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region "Service"
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IRecommendationService, RecommendationService>();
            services.AddTransient<IFinancialGoalService, FinancialGoalService>();
            services.AddTransient<IChatbotSessionService, ChatbotSessionService>();
            services.AddTransient<IChatbotMessageService, ChatbotMessageService>();
            #endregion
        }
    }
}
