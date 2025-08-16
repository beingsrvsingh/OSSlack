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
    }
}
