using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Common.Behaviours;

namespace CatalogUI.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());                
            });

            return services;
        }
    }
}