using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Query
{
    public class GetTrendingProductQuery : IRequest<Result>
    {
        public int Cid { get; set; }
        public int Records { get; set; }
    }
}