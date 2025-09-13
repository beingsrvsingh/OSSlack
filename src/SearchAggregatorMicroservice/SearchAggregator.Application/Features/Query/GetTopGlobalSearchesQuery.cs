using MediatR;
using SearchAggregator.Domain.Entities;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.Query
{
    public class GetTopGlobalSearchesQuery : IRequest<Result>
    {
        public int TopN { get; }

        public GetTopGlobalSearchesQuery(int topN = 5)
        {
            TopN = topN;
        }
    }

}
