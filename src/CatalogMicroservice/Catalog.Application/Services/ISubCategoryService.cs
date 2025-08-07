using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryMaster>> GetAllSubCategoriesAsync();
        Task<SubCategoryMaster?> GetSubCategoryByIdAsync(int id);
        Task<bool> CreateSubCategoryAsync(SubCategoryMaster subCategory);
        Task<bool> UpdateSubCategoryAsync(SubCategoryMaster subCategory);
        Task<bool> DeleteSubCategoryAsync(int id);

        Task<IEnumerable<SubCategoryLocalizedText>> GetLocalizedTextsAsync(int subCategoryId);
        Task<bool> AddOrUpdateLocalizedTextAsync(SubCategoryLocalizedText localizedText);
    }
}