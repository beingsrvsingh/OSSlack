using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        private readonly ICategoryLocalizedTextRepository _localizedTextRepository;
        private readonly ILoggerService<CategoryService> _logger;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ICategoryLocalizedTextRepository localizedTextRepository,
            ISubCategoryRepository subCategoryRepository,
            ILoggerService<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _localizedTextRepository = localizedTextRepository;
            this.subCategoryRepository = subCategoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<CategoryMaster?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<List<SubCategoryMaster>> GetSubCategoriesByCategoryIdAsync(int id)
        {
            var result = await subCategoryRepository.GetAsync(s => s.CategoryMasterId == id);
            return result.ToList();
        }


        public async Task<bool> CreateCategoryAsync(CategoryMaster category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create category", ex);
                return false;
            }
        }

        public async Task<bool> UpdateCategoryAsync(CategoryMaster category)
        {
            try
            {
                await _categoryRepository.UpdateAsync(category);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update category", ex);
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete category with ID: {id}", ex);
                return false;
            }
        }

        public async Task<IEnumerable<CategoryLocalizedText>> GetLocalizedTextsAsync(int categoryId)
        {
            return await _localizedTextRepository.GetAsync(x => x.CategoryId == categoryId);
        }

        public async Task<bool> AddOrUpdateLocalizedTextAsync(CategoryLocalizedText localizedText)
        {
            try
            {
                var existing = await _localizedTextRepository
                    .FirstOrDefaultAsync(x => x.CategoryId == localizedText.CategoryId &&
                                              x.LanguageCode == localizedText.LanguageCode);
                if (existing != null)
                {
                    existing.LocalizedName = localizedText.LocalizedName;
                    existing.LocalizedDescription = localizedText.LocalizedDescription;
                    await _localizedTextRepository.UpdateAsync(existing);
                }
                else
                {
                    await _localizedTextRepository.AddAsync(localizedText);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add/update localized text for category", ex);
                return false;
            }
        }
    }
}
