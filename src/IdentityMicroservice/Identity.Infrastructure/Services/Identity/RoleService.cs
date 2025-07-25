using Identity.Application.Features.Admin.Commands;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces.Logging;

namespace Identity.Infrastructure.Services.Identity
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILoggerService<RoleService> _logger;
        public RoleService(ILoggerService<RoleService> loggerService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this._logger = loggerService;
            this.userManager = userManager;
        }

        public async Task<bool> AddUserRoleByEmailAsync(UserEmailRoleCommand userEmailRoleCommand)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userEmailRoleCommand.Email);

                if (user is null && user?.Email is null)
                {
                    _logger.LogWarning("User not found with email {Email}", userEmailRoleCommand.Email);
                    return false;
                }

                var result = await userManager.AddToRoleAsync(user, userEmailRoleCommand.RoleName);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to add role {RoleName} to user {Email}: {Errors}",
                        userEmailRoleCommand.RoleName, user.Email ?? "[null]",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding user role");
                return false;
            }
        }

        public async Task<bool> AddUserRoleByPhoneAsync(UserPhoneRoleCommand userPhoneRoleCommand)
        {
            try
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == userPhoneRoleCommand.PhoneNumber);

                if (user is null)
                {
                    _logger.LogWarning("User not found with phone number {Phone}", userPhoneRoleCommand.PhoneNumber);
                    return false;
                }

                var result = await userManager.AddToRoleAsync(user, userPhoneRoleCommand.RoleName);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to add role {RoleName} to user with phone {Phone}: {Errors}",
                        userPhoneRoleCommand.RoleName, user.PhoneNumber ?? "[null]",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding user role by phone");
                return false;
            }
        }
    }
}

