using MediatR;
using Product.Domain.Entities;

namespace Product.Application.Features.Query
{
    public class GetProductSEOInfoQuery : IRequest<ProductSEOInfoMaster?>
    {
        public int ProductId { get; }

        public GetProductSEOInfoQuery(int productId)
        {
            ProductId = productId;
        }
    }

}