using MediatR;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.Query
{
    public class GetTrendingTempleQuery : IRequest<Result>
    {
    }
}
