using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application;
using Product.Infrastructure.Persistence.Context;
using Shared.Utilities;

namespace Product.Infrastructure
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

            services.AddDbContext<ProductDbContext>(options =>
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
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ProductDbContext>>();

            using (var dbContext = new ProductDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}