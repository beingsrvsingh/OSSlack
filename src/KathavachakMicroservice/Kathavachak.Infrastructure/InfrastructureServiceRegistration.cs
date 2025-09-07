using Kathavachak.Application;
using Kathavachak.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Utilities;

namespace Kathavachak.Infrastructure
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

            services.AddDbContext<KathavachakDbContext>(options =>
                    options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 28)),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddApplicationServices();

            return services;
        }

        //To apply migrations programmatically, call context.Database.Migrate()
        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<KathavachakDbContext>>();

            using (var dbContext = new KathavachakDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}