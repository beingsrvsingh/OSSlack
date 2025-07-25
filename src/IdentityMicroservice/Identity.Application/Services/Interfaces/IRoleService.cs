using Identity.Application.Features.Admin.Commands;

namespace Identity.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AddUserRoleByEmailAsync(UserEmailRoleCommand userEmailRoleCommand);

        Task<bool> AddUserRoleByPhoneAsync(UserPhoneRoleCommand userPhoneRoleCommand);
    }
}
