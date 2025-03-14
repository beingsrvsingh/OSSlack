using Identity.Infrastructure.Constants;
using Shared.Infrastructure.Health;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Shared.Infrastructure.Extensions
{
    public static class AddHealthCheckExtensions
    {
        public static void AddAllHealthChecks(this IServiceCollection services, string connectionString)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSingleton<ReadinessHealthCheck>();

            services.AddHealthChecks()
                    .AddTypeActivatedCheck<DbConnectionHealthCheck>(
                        "Database",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new[] { HealthCheckTags.Database },
                        args: new object[] { connectionString })
                    .AddCheck<ReadinessHealthCheck>("Readiness", tags: new[] { HealthCheckTags.Ready });

            // This is has been disabled until add support for .NET 8.0 and EF Core 8.0
            // services.AddHealthChecksUI().AddInMemoryStorage();
        }
    }
}
