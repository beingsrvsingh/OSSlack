using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetPoojaKitItemLocalizedTextsQuery(int ItemId) : IRequest<Result>;

}