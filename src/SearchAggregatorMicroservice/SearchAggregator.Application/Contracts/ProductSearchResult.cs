using SearchAggregator.Application.Contracts.Dtos;
using Shared.Application.Contracts;

namespace SearchAggregator.Application.Contracts
{
    public class ProductSearchResult : SearchResult<ProductDto>
    {
        public ProductSearchResult()
        {
            Source = "Product";
        }
    }
}
