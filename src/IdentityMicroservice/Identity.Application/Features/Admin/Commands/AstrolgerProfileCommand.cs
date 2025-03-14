using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class AstrolgerProfileCommand : IRequest<Result>
    {
        public required string Address { get; set; }
    }
}
