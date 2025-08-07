using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public record DeletePoojaKitCommand(int Id) : IRequest<Result>;

}