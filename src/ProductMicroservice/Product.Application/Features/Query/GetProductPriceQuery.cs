using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetProductPriceQuery : IRequest<Result>
    {
        public int ProductId { get; set; }
    }
}
