using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Query
{
    public class GetHighlightTrendingProductQuery : IRequest<Result>
    {
        public int Cid { get; set; }
        public int Records { get; set; }
    }
}