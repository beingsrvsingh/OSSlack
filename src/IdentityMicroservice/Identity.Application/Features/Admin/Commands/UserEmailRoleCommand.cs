using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class UserEmailRoleCommand : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string RoleName { get; set; }
    }
}
