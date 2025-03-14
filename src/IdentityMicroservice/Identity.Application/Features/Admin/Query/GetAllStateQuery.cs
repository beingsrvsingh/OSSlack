using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public record GetAllStateQuery : IRequest<IResult>
    {
    }
}
