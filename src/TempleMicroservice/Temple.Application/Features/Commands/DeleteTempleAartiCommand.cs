using MediatR;
using Shared.Utilities.Response;
using Temple.Application.Contracts;

namespace Temple.Application.Features.Commands
{
    public record DeleteTempleAartiCommand(int Id) : IRequest<Result>;

}
