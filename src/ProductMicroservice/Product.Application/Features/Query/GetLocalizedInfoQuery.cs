using MediatR;
using Product.Domain.Entities;

namespace Product.Application.Features.Query
{
    public class GetLocalizedInfoQuery : IRequest<IEnumerable<LocalizedProductInfoMaster>>
    {
        public int ProductId { get; }

        public GetLocalizedInfoQuery(int productId)
        {
            ProductId = productId;
        }
    }

}