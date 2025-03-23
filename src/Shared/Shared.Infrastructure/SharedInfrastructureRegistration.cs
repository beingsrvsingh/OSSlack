using Identity.Application.Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Shared.Application.Common.Services.Interfaces;
using Shared.Domain.Repository;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Repositories;
using Shared.Infrastructure.Services;
using Shared.Utilities.Services;
using Utilities;
using Utilities.Services;

namespace Shared.Infrastructure
{
    public static class SharedInfrastructureRegistration
    {
        public static IServiceCollection AddSharedInfrastructureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddHttpClient<ILoggingApiClient, LoggingApiClient>("LoggerMicroservice", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Config.LoggerBaseApiGatewayUri);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");          

                var httpContext = services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>().HttpContext;                
                Utitlities.AddHeadersToken(httpClient, httpContext);                

            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(3)).
            AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)));

            return services;
        }
    }
}
