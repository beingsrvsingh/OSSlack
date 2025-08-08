using MediatR;
using Product.Domain.Entities;

namespace Product.Application.Features.Query
{
    public class GetProductAttributesQuery : IRequest<IEnumerable<ProductAttributeMaster>>
    {
        public int ProductId { get; }

        public GetProductAttributesQuery(int productId)
        {
            ProductId = productId;
        }
    }

}