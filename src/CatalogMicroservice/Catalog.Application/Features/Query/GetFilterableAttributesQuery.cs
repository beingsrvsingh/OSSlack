using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetFilterableAttributesQuery(int CategoryId, int SubCategoryId, bool IsFilterable = true) : IRequest<Result>
    {
        
    }
}