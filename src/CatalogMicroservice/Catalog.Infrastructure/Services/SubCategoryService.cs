using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISubCategoryLocalizedTextRepository _localizedTextRepository;
        private readonly ILoggerService<SubCategoryService> _logger;

        public SubCategoryService(
            ISubCategoryRepository subCategoryRepository,
            ISubCategoryLocalizedTextRepository localizedTextRepository,
            ILoggerService<SubCategoryService> logger)
        {
            _subCategoryRepository = subCategoryRepository;
            _localizedTextRepository = localizedTextRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SubCategoryMaster>> GetAllSubCategoriesAsync()
        {
            try
            {
                return await _subCategoryRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching all subcategories", ex);
                return Enumerable.Empty<SubCategoryMaster>();
            }
        }

        public async Task<SubCategoryMaster?> GetSubCategoryByIdAsync(int id)
        {
            try
            {
                return await _subCategoryRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching subcategory with id {id}", ex);
                return null;
            }
        }

        public async Task<bool> CreateSubCategoryAsync(SubCategoryMaster subCategory)
        {
            try
            {
                await _subCategoryRepository.AddAsync(subCategory);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating subcategory", ex);
                return false;
            }
        }

        public async Task<bool> UpdateSubCategoryAsync(SubCategoryMaster subCategory)
        {
            try
            {
                await _subCategoryRepository.UpdateAsync(subCategory);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating subcategory with id {subCategory.Id}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteSubCategoryAsync(int id)
        {
            try
            {
                var entity = await _subCategoryRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"Subcategory with id {id} not found for deletion");
                    return false;
                }
                await _subCategoryRepository.DeleteAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting subcategory with id {id}", ex);
                return false;
            }
        }

        public async Task<IEnumerable<SubCategoryLocalizedText>> GetLocalizedTextsAsync(int subCategoryId)
        {
            try
            {
                return await _localizedTextRepository.GetBySubCategoryIdAsync(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching localized texts for subcategory id {subCategoryId}", ex);
                return Enumerable.Empty<SubCategoryLocalizedText>();
            }
        }

        public async Task<bool> AddOrUpdateLocalizedTextAsync(SubCategoryLocalizedText localizedText)
        {
            try
            {
                var existing = await _localizedTextRepository.GetByIdAsync(localizedText.Id);
                if (existing == null)
                {
                    await _localizedTextRepository.AddAsync(localizedText);
                }
                else
                {
                    existing.LanguageCode = localizedText.LanguageCode;
                    existing.LocalizedName = localizedText.LocalizedName;
                    existing.LocalizedDescription = localizedText.LocalizedDescription;
                    await _localizedTextRepository.UpdateAsync(existing);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding/updating localized text", ex);
                return false;
            }
        }
    }

}