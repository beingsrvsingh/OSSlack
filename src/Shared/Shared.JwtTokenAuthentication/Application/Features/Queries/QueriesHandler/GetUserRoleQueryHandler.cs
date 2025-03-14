using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Shared.JwtTokenAuthentication.Application.Features.Queries.QueriesHandler
{
    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, bool>
    {
        private readonly UserManager<IdentityUser> userManager;

        public GetUserRoleQueryHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            var isRole = await userManager.IsInRoleAsync(user!, request.Role);

            if (user is null && !isRole)
                return false;

            return true;
        }
    }
}
