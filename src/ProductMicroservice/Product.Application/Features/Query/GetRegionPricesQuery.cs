using MediatR;
using Product.Domain.Entities;

namespace Product.Application.Features.Query
{
    public class GetRegionPricesQuery : IRequest<IEnumerable<ProductRegionPriceMaster>>
    {
        public int ProductId { get; }

        public GetRegionPricesQuery(int productId)
        {
            ProductId = productId;
        }
    }

}