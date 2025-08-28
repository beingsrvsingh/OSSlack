
using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
        Task<IEnumerable<CatalogAttributeGroupDto>> GetGroupedAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
        Task<IEnumerable<FilterableAttributeDto>> GetFilterableAttributeById(int categoryId, int subCategoryId, bool isSummary = false);
    }

}