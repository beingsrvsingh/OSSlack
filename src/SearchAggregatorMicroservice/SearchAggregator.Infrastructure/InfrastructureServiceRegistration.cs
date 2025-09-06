using Microsoft.Extensions.DependencyInjection;
using SearchAggregator.Application;

namespace SearchAggregator.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddApplicationServices();

            return services;
        }
    }
}