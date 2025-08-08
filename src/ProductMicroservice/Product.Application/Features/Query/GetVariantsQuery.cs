using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetVariantsQuery : IRequest<Result>
    {
        public int ProductId { get; }

        public GetVariantsQuery(int productId)
        {
            ProductId = productId;
        }
    }

}