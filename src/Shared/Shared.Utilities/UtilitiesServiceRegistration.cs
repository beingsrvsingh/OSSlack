using Microsoft.Extensions.DependencyInjection;
using Utilities.Services;

namespace Utilities
{
    public static class UtilitiesServiceRegistration
    {
        public static IServiceCollection AddUtilitiesServiceRegistration(this IServiceCollection services)
        {            
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<ISecurityService, SecurityService>();            

            return services;
        }
    }
}
