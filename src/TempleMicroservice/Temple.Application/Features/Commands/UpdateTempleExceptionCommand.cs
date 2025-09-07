using MediatR;
using Shared.Utilities.Response;
using Temple.Application.Contracts;

namespace Temple.Application.Features.Commands
{
    public record UpdateTempleExceptionCommand(int Id, TempleExceptionDto ExceptionDto) : IRequest<Result>;

}
