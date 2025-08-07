using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public record DeletePoojaKitItemCommand(int Id) : IRequest<Result>;

}