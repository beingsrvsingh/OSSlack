using Catalog.Application.Contracts;
using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync();
        Task<CategoryMaster?> GetCategoryByIdAsync(int id);
        Task<List<SubCategoryMaster>> GetSubCategoriesByCategoryIdAsync(int id);
        Task<bool> CreateCategoryAsync(CategoryMaster category);
        Task<bool> UpdateCategoryAsync(CategoryMaster category);
        Task<bool> DeleteCategoryAsync(int id);

        Task<IEnumerable<CategoryLocalizedText>> GetLocalizedTextsAsync(int categoryId);
        Task<bool> AddOrUpdateLocalizedTextAsync(CategoryLocalizedText localizedText);
        Task<IEnumerable<CatalogAttributeDto>> GetAttributesAsync(int categoryId, int subCategoryId, bool summaryOnly = false);
        Task<IEnumerable<CatalogAttributeGroupDto>> GetGroupedAttributesAsync(int categoryId, int subCategoryId, bool summaryOnly = false);
        Task<List<FilterableAttributeDto>> GetFilterableAttributes(int categoryId, int subCategoryId);

    }
}
