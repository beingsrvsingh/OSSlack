using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public record GetProductsBySubcategoryIdQuery : IRequest<Result>
    {
        public int SubCategoryId { get; set; }
        public bool IsSummary = false;
        public int Records { get; set; } = 5;
    }
}