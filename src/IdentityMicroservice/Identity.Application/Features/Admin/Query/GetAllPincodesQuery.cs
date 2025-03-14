using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public record GetAllPincodesQuery : IRequest<Result>
    {
    }
}
