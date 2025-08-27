using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetAttributesByCategoryIdQuery(int CategoryId, int SubCategoryId, bool IsSummary = false) : IRequest<Result>;


}