using Logging.Applicaton.Service;
using Logging.Domain.Repositories;
using Logging.Domain.Service;
using Logging.Domain.UOW;
using Logging.Infrastructure.Context;
using Logging.Infrastructure.Persistence.Repository;
using Logging.Infrastructure.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Review.Application;
using Review.Infrastructure.Persistence.Context;
using Shared.Utilities;

namespace Logging.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var config = Helper.LoadAppSettings();

            services.AddDbContext<LoggerContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAppsLogService, AppsLogService>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IAppsLogRepository, AppsLogRepository>();

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
