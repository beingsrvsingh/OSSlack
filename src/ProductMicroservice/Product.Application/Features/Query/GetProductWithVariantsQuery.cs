using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetProductWithVariantsQuery : IRequest<Result>
    {
        public int ProductId { get; }

        public GetProductWithVariantsQuery(int productId)
        {
            ProductId = productId;
        }
    }

}