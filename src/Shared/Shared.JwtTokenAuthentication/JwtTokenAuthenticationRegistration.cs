using JwtTokenAuthentication.Application;
using JwtTokenAuthentication.Application.Interfaces;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Infrastructure.Persistence.Context;
using JwtTokenAuthentication.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.JwtTokenAuthentication.Middleware;
using Utilities.Cryptography;
using Utilities.Interfaces;
using Utilities.Services;

namespace JwtTokenAuthentication
{
    public static class JwtTokenAuthenticationRegistration
    {
        private static IRegistryService? registryService = null;
        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services)
        {
            services.AddJwtAuthentication();
            services.AddJwtAuthorization();

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddScoped<IRegistryService, RegistryService>();
            services.AddScoped<ITokenDbContext, TokenDbContext>();
            services.AddScoped<IJwtService, JwtService>();

            registryService = services.BuildServiceProvider().GetRequiredService<IRegistryService>();

            services.AddDbContext<TokenDbContext>();

            //services.Configure<AppSettings>(appSettings.GetSection("AppSettings"));
            //services.AddScoped(cfg => cfg.GetService<IOptions<AppSettings>>()!.Value);                        

            return services;
        }

        //To apply migrations programmatically, call context.Database.Migrate()
        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<TokenDbContext>>();

            using (var dbContext = new TokenDbContext(dbContextOptions, registryService!))
            {
                dbContext?.Database?.Migrate();
            }
        }

        public static void MigrateConnectionString(IServiceProvider serviceProvider)
        {
            if (registryService is not null)
            {
                string keyName = registryService.ConnectionStringKeyName;
                string connectionString = "Server=DESKTOP-NSNUU6Q;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;";

                if (!string.IsNullOrEmpty(keyName) && string.IsNullOrEmpty(registryService.GetConnectionString()))
                {
                    string encryptConnectionString = Cryptography.EncryptString(connectionString);
                    registryService.SetValue(keyName, encryptConnectionString);
                }

                //string securityKeyName = registryService.TokenSeurityKeyName;

                //if (!string.IsNullOrEmpty(securityKeyName) && string.IsNullOrEmpty(registryService.GetSecurityKey()))
                //{
                //    string encryptSecurityKey = Cryptography.EncryptString(JwtConstant.JWT_TOKEN_SECURITYKEY);
                //    registryService.SetValue(securityKeyName, encryptSecurityKey);
                //}
            }
        }
    }
}
