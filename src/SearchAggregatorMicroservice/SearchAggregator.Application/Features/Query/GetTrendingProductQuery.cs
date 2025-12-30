using MediatR;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.Query
{
    public class GetTrendingProductQuery : IRequest<Result>
    {
    }
}
