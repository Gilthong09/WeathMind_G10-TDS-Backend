using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
            
            #endregion
        }
    }
}
