using Catalog.Application;
using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Core.UOW;
using Catalog.Infrastructure.Persistence.Context;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Repositories.UOW;
using Catalog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Model;
using Shared.Infrastructure;
using Shared.Utilities;
using System.Reflection;

namespace Catalog.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var config = Helper.LoadAppSettings();

            services.AddDbContext<CatalogDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            services.AddApplicationServices();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICatalogService, CatalogService>();

            services.Configure<MongoDbAppSettings>(config.GetSection("MongoDb"));
            services.AddSingleton<SampleMongoRepository>();

            return services;
        }

        //To apply migrations programmatically, call context.Database.Migrate()
        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<CatalogDbContext>>();

            using (var dbContext = new CatalogDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
