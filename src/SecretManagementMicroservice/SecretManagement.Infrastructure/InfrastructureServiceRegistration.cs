using SecretManagement.Application;
using SecretManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Utilities;
using System.Reflection;

namespace SecretManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var config = Helper.LoadAppSettings();

            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string 'DefaultConnection' cannot be null or empty.");
            }
            services.AddDbContext<SecretManagementDbContext>(options =>
                    options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 28)),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            //Default Services    
            services.AddApplicationServices();

            return services;
        }

        //To apply migrations programmatically, call context.Database.Migrate()
        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<SecretManagementDbContext>>();

            using (var dbContext = new SecretManagementDbContext(dbContextOptions))
            {
                if (dbContext is not null)
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
