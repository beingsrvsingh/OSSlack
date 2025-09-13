
using SearchAggregator.Application.Contracts;

namespace SearchAggregator.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
    }

}