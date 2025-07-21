using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Interfaces;
using Shared.JwtTokenAuthentication.Middleware;

namespace JwtTokenAuthentication
{
    public static class JwtTokenAuthenticationRegistration
    {
        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IPlatformService platformService)
        {
            services.AddJwtAuthentication(platformService);
            services.AddJwtAuthorization();

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();                      

            return services;
        }
    }
}
