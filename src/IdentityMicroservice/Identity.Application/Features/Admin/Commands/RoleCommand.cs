using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class RoleCommand : IRequest<Result>
    {
        public required List<string> Roles { get; set; }
    }
}
