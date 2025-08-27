using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetProductQuery : IRequest<Result>
    {
        public int productId { get; init; }
        //Catalog MS payload
        public int CategoryId { get; init; }
        public int SubCategoryId { get; init; }
        public bool IsSummary { get; init; } = false;
    }
}