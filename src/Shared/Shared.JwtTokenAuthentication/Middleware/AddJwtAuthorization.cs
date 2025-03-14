using JwtTokenAuthentication.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.JwtTokenAuthentication.Middleware
{
    public static class AddJwtAuthorizationExtensions
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtConstant.JWT_TOKEN_SCOPE, policy => policy.Requirements.Add(new HasScopeRequirement(JwtConstant.JWT_TOKEN_SCOPE, JwtConstant.JWT_TOKEN_ISSUER)));
            });

            return services;
        }
    }
}
