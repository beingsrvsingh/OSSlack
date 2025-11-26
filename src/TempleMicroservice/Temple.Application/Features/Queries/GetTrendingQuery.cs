using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Query
{
    public class GetTrendingQuery : IRequest<Result>
    {
        public int? Scid { get; set; }
        public int Records { get; set; } = 5;
    }
}