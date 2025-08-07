using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Queries
{
    public record GetAllPoojaKitsQuery() : IRequest<Result>;

}
