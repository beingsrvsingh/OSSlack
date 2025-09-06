using Amazon.Auth.AccessControlPolicy;
using SearchAggregator.Application.Contracts.Dtos;
using Shared.Application.Contracts;

namespace SearchAggregator.Application.Contracts
{
    public class PriestSearchResult : SearchResult<PriestDto>
    {
        public PriestSearchResult()
        {
            Source = "Priest";
        }
    }
}
