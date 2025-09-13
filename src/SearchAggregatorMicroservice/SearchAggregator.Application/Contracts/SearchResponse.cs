using SearchAggregator.Application.Contracts.Dtos;
using Shared.Application.Contracts;

namespace SearchAggregator.Application.Contracts
{
    public class SearchResponse
    {        
        public List<SearchResponseDto>? Results { get; set; } = new();        
        public FilterMetadata Filters { get; set; } = new();

    }
}
