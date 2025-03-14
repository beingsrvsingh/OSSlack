using Identity.Application.Features.Admin.Commands;

namespace Identity.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task AddRoles(RoleCommand roleCommand);

        Task AddUserRoles(UserRoleCommand userRoleCommand);
    }
}
