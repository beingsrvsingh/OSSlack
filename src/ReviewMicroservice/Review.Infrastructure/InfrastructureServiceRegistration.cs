﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Review.Application;
using Review.Application.Services;
using Review.Domain.Core.UOW;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Review.Infrastructure.Repositories;
using Review.Infrastructure.Services;
using Shared.Infrastructure;
using Shared.Infrastructure.Repositories;
using Shared.Utilities;
using System.Reflection;
using Utilities;

namespace Review.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var config = Helper.LoadAppSettings();

            services.AddDbContext<ReviewDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            //DI
            services.AddApplicationServices();
            services.AddSharedInfrastructureDependencyInjection();

            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewDetailService, ReviewDetailService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewDetailRepository, ReviewDetailRepository>();
            services.AddScoped<IReportLookUpRepository, ReportLookUpRepository>();

            services.AddHttpClient<IIdentityAPIClient, IdentityAPIClient>("IdentityAPIMicroservice", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:7190");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Review-Microservice");

                var httpContext = services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>().HttpContext;

                httpClient = Utitlities.AddHeadersToken(httpClient, httpContext);

            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(3)).
            AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)));

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
