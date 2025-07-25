using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class UserPhoneRoleCommand : IRequest<Result>
    {
        public required string PhoneNumber { get; set; }
        public required string RoleName { get; set; }
    }
}
