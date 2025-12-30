using MediatR;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.Query
{
    public class GetTrendingAstrologerQuery : IRequest<Result>
    {
    }
}
