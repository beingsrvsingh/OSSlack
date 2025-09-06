using SearchAggregator.Application.Contracts.Dtos;
using Shared.Application.Contracts;

namespace SearchAggregator.Application.Contracts
{
    public class KathavachakSearchResult : SearchResult<KathavachakDto>
    {
        public KathavachakSearchResult()
        {
            Source = "Kathavachak";
        }
    }
}
