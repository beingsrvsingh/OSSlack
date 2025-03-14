using Identity.Application.Features.Admin.Commands;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Common.Services.Interfaces;

namespace Identity.Infrastructure.Services.Identity
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILoggerService loggerService;
        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILoggerService loggerService)
        {
            this.roleManager = roleManager;
            this.loggerService = loggerService;
            this.userManager = userManager;
        }

        public async Task AddRoles(RoleCommand roleCommand)
        {
            ApplicationUser aspNetUsers = new ApplicationUser();
            var Role = roleCommand.Roles;
            foreach (var role in Role)
            {
                bool isExistRole = await roleManager.RoleExistsAsync(role);
                if (!isExistRole)
                {
                    var identityRole = new IdentityRole();
                    identityRole.Name = role;
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }

        public async Task AddUserRoles(UserRoleCommand userRoleCommand)
        {
            var aspNetUsers = await userManager.FindByEmailAsync(userRoleCommand.Email);

            if (aspNetUsers is not null)
            {
                await userManager.AddToRoleAsync(aspNetUsers, userRoleCommand.RoleName);
                return;
            }       
        }
    }
}

