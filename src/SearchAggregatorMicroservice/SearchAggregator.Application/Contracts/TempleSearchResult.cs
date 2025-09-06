using SearchAggregator.Application.Contracts.Dtos;
using Shared.Application.Contracts;

namespace SearchAggregator.Application.Contracts
{
    public class TempleSearchResult : SearchResult<KathavachakDto>
    {
        public TempleSearchResult()
        {
            Source = "Kathavachak";
        }
    }
}
