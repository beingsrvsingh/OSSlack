using Catalog.Application.Contracts;
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
        private readonly ICatalogAttributeRepository attributeRepository;
        private readonly ICategoryLocalizedTextRepository _localizedTextRepository;
        private readonly ILoggerService<CategoryService> _logger;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ICategoryLocalizedTextRepository localizedTextRepository,
            ISubCategoryRepository subCategoryRepository,
            ICatalogAttributeRepository attributeRepository,
            ILoggerService<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _localizedTextRepository = localizedTextRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.attributeRepository = attributeRepository;
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
                await _categoryRepository.SaveChangesAsync();                            
                return true;
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, "Failed to create category with attributes");
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

        public async Task<IEnumerable<CatalogAttributeDto>> GetAttributesByCategoryIdAsync(int categoryId)
        {
            var attributes = await attributeRepository
                .GetAttributesByCategoryIdAsync(categoryId);

            return attributes.Select(attr => new CatalogAttributeDto
            {
                Id = attr.Id,
                Key = attr.Key,
                Label = attr.Label,
                DataType = attr.DataType.ToString(),
                IsCustom = attr.IsCustom,
                IsRequired = attr.IsRequired,
                SortOrder = attr.SortOrder,
                AllowedValues = attr.AllowedValues?
                .OrderBy(v => v.SortOrder)
                .Select(v => new CatalogAttributeAllowedValueDto
                {
                    Id = v.Id,
                    Value = v.Value,
                    SortOrder = v.SortOrder
                }).ToList(),
                Icon = new CatalogAttributeIconDto
                {
                    IconCodePoint = attr.CatalogAttributeIcon?.IconCodePoint,
                    IconFontFamily = attr.CatalogAttributeIcon?.IconFontFamily,
                    IconName = attr.CatalogAttributeIcon?.IconName,
                },
            });
        }
    }
}
