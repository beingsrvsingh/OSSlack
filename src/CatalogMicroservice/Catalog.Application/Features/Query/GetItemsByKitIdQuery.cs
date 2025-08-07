using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetItemsByKitIdQuery(int PoojaKitId) : IRequest<Result>;

}