using MediatR;
using Shared.Utilities.Response;
using Temple.Application.Contracts;

namespace Temple.Application.Features.Commands
{
    public record CreateTempleMasterCommand(TempleMasterDto TempleMasterDto) : IRequest<Result>;

}
