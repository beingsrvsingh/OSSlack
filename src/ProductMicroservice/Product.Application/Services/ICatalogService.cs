
using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogAttributeGroupDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
    }

}