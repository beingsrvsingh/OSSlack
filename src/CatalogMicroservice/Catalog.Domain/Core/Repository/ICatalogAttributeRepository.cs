using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ICatalogAttributeRepository : IRepository<CatalogAttribute>
    {
        Task<List<CatalogAttribute>> GetAttributesByCategoryOrSubCategoryAsync(int categoryId, int subCategoryId, bool summaryOnly = false);
        Task<List<CatalogAttributeRaw>> GetFilterableAttributes(int categoryId, int subCategoryId);
    }

}