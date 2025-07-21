using Identity.Application;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.Repository;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Utilities;
using System.Reflection;

namespace Identity.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var config = Helper.LoadAppSettings();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            //Default Services
            services.AddApplicationServices();   

            //add identity
            var builder = services.AddIdentityCore<ApplicationUser>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.AddUserManager<UserManager<ApplicationUser>>();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();
            builder.AddRoles<IdentityRole>();


            services.Configure<DataProtectionTokenProviderOptions>(opt =>
               opt.TokenLifespan = TimeSpan.FromHours(1));

            //The number of iterations used when hashing passwords using PBKDF2.This value is only used when the CompatibilityMode is set to IdentityV3.The value must be a positive integer and defaults to 10000
            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });            

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISeedService, SeedService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICountryMasterRepository, CountryMasterRepository>();
            services.AddScoped<IStateMasterRepository, StateMasterRepository>();
            services.AddScoped<ICityMasterRepository, CityMasterRepository>();

            return services;
        }

        //To apply migrations programmatically, call context.Database.Migrate()
        //https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

            using (var dbContext = new ApplicationDbContext(dbContextOptions))
            {
                if(dbContext is not null)
                {
                    dbContext.Database.Migrate();
                    MigrateRoles(dbContext);
                }
            }
        }

        private static void MigrateRoles(ApplicationDbContext dbContext)
        {
            string[] Role = IIdentityRole.roles();
            string ConcurrencyId = Guid.NewGuid().ToString();
            foreach (var role in Role)
            {
                //TODO Caching
                bool isExist = dbContext.Roles.AnyAsync(r => r.Name == role).Result;
                if (!isExist)
                {
                    IdentityRole identity = new IdentityRole();
                    identity.Name = role;
                    identity.NormalizedName = role.ToLower();
                    identity.ConcurrencyStamp = ConcurrencyId;
                    dbContext.Roles.Add(identity);
                    dbContext.SaveChanges();

                }
            }
        }
    }
}
