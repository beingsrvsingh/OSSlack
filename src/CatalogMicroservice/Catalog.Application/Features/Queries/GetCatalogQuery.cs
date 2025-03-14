using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Queries
{
    public record class GetCatalogQuery : IRequest<Result>
    {
    }
}
