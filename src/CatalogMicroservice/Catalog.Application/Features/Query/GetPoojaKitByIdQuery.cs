using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Queries.QueryHandlers
{
    public record GetPoojaKitByIdQuery(int Id) : IRequest<Result>;
}
