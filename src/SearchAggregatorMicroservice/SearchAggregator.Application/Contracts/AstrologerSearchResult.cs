using SearchAggregator.Application.Contracts.Dtos;
using Shared.Application.Contracts;

namespace SearchAggregator.Application.Contracts
{
    public class AstrologerSearchResult : SearchResult<AstrologerDto>
    {
        public AstrologerSearchResult()
        {
            Source = "Priest";
        }
    }
}
