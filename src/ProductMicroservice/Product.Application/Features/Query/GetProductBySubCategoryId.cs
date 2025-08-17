using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public record GetProductBySubCategoryId : IRequest<Result>
    {
        public int SubCategoryId { get; set; }
    }
}