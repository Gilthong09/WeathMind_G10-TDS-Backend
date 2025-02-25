using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services;
using System.Reflection;

namespace WealthMind.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region "Service"
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
