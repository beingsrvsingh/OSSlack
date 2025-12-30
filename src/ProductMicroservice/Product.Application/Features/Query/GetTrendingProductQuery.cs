using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetTrendingProductQuery : IRequest<Result>
    {
        public int? Scid { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}