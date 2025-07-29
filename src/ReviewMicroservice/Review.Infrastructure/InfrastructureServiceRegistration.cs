using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Review.Application;
using Review.Application.Services;
using Review.Infrastructure.Persistence.Context;
using Review.Infrastructure.Services;
using Review.Infrastructure.Utils.Constants;
using Shared.Utilities;
using System.Reflection;

namespace Review.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var config = Configuration.LoadAppSettings();
            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string 'DefaultConnection' cannot be null or empty.");
            }

            services.AddDbContext<ReviewDbContext>(options =>
                    options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 28)),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            services.AddHttpClient<IIdentityApiClient, IdentityApiClient>("IdentityAPIMicroservice", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Config.IdentityBaseApiGatewayUri);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var httpContext = services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>().HttpContext;

                Shared.Utilities.Utils.AddHeadersToken(httpClient, httpContext);

            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(3)).
            AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)));

            services.AddApplicationServices();

            return services;
        }

        //To apply migrations programmatically, call context.Database.Migrate()
        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ReviewDbContext>>();

            using (var dbContext = new ReviewDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
