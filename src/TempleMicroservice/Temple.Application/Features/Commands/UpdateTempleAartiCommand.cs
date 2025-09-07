using MediatR;
using Shared.Utilities.Response;
using Temple.Application.Contracts;

namespace Temple.Application.Features.Commands
{
    public record UpdateTempleAartiCommand(int Id, TempleAartiDto AartiDto) : IRequest<Result>;

}
