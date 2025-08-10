using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Commands
{
    public record DeleteTempleCommand(int Id) : IRequest<Result>;
}