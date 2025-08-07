using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public record UpdatePoojaKitCommand(PoojaKitMaster PoojaKit) : IRequest<Result>;

}