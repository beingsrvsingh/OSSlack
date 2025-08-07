using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public record UpdatePoojaKitItemCommand(PoojaKitItemMaster Item) : IRequest<Result>;

}