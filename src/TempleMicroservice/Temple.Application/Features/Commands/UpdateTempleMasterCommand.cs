using MediatR;
using Shared.Utilities.Response;
using Temple.Application.Contracts;

namespace Temple.Application.Features.Commands
{
    public record UpdateTempleMasterCommand(int Id, TempleMasterDto TempleMasterDto) : IRequest<Result>;

}
