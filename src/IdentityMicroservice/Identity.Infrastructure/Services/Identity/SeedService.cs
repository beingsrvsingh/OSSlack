using Identity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Interfaces.Logging;

namespace Identity.Infrastructure.Services.Identity
{
    public class SeedService : ISeedService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILoggerService loggerService;
        public SeedService(RoleManager<IdentityRole> roleManager, ILoggerService loggerService)
        {
            this.roleManager = roleManager;
            this.loggerService = loggerService;
        }

        public async Task CreateRoleSync()
        {
            loggerService.LogInfo("Creating roles");
            string[] Role = IIdentityRole.roles();
            foreach (var role in Role)
            {
                bool isExist = await roleManager.RoleExistsAsync(role);
                if (!isExist)
                {
                    IdentityRole identity = new IdentityRole();
                    identity.Name = role;
                    await roleManager.CreateAsync(identity);
                    loggerService.LogInfo("Created role {0}", role);
                }
            }
        }
    }
}
