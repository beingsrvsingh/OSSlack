using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public record GetProductsWithAttributesQuery : IRequest<Result>
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsSummary = false;
    }
}