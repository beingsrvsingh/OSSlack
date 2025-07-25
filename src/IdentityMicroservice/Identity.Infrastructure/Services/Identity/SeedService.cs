using Identity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Interfaces.Logging;

namespace Identity.Infrastructure.Services.Identity
{
    public class SeedService : ISeedService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoggerService<SeedService> _logger;

        public SeedService(ILoggerService<SeedService> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleSync()
        {
            _logger.LogInfo("Starting role creation...");

            var roles = IIdentityRole.roles();
            var allSucceeded = true;

            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                    if (result.Succeeded)
                    {
                        _logger.LogInfo("Created role: {0}", roleName);
                    }
                    else
                    {
                        allSucceeded = false;
                        _logger.LogError("Failed to create role: {0}. Errors: {1}",
                            roleName,
                            string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    _logger.LogInfo("Role already exists: {0}", roleName);
                }
            }

            _logger.LogInfo("Role creation process finished.");
            return allSucceeded;
        }
    }
}
