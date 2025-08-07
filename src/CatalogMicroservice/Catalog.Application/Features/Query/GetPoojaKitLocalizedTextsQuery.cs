using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetPoojaKitLocalizedTextsQuery(int KitId) : IRequest<Result>;

}