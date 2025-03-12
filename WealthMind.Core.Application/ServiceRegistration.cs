using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services;

namespace WealthMind.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region "Service"
            services.AddTransient<ICashService, CashService>();
            services.AddTransient<ISavingService, SavingService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IInvestmentService, InvestmentService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ICreditCardService, CreditCardService>();

            #endregion
        }
    }
}
