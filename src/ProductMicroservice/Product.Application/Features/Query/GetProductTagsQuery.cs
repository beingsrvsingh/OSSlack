using MediatR;
using Product.Domain.Entities;

namespace Product.Application.Features.Query
{
    public class GetProductTagsQuery : IRequest<IEnumerable<ProductTagMaster>>
    {
        public int ProductId { get; }

        public GetProductTagsQuery(int productId)
        {
            ProductId = productId;
        }
    }

}