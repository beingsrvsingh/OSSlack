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

        public async Task<List<CategoryParentResponseDto>> GetParentSubcategoriesAsync()
        {
            var raw = await _categoryRepository.GetParentSubcategoriesRawAsync();

            var result = raw
                .GroupBy(x => x.CategoryName)
                .Select(g => new CategoryParentResponseDto
                {
                    CategoryName = g.Key,
                    Subcategories = g.Select(s => new SubCategoryParentResponseDto
                    {
                        Id = s.SubcategoryId,
                        Name = s.SubcategoryName,
                        ResourceType = s.ResourceType
                    }).ToList()
                })
                .ToList();

            return result;
        }

        public async Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAsync(c => c.ParentCategoryId != null) ?? Enumerable.Empty<CategoryMaster>();
        }

        public async Task<CategoryMaster?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<List<SubCategoryMaster>> GetSubCategoriesByCategoryIdAsync(int id)
        {
            var result = await subCategoryRepository.GetAsync(s => s.ParentSubcategoryId == id);
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

        public async Task<IEnumerable<CatalogAttributeDto>> GetAttributesAsync(int categoryId, int subCategoryId, bool summaryOnly = false)
        {
            var attributes = await attributeRepository
                .GetAttributesByCategoryOrSubCategoryAsync(categoryId, subCategoryId, summaryOnly);

            var dtos = attributes.Select(attr => new CatalogAttributeDto
            {
                Id = attr.Id,
                Key = attr.CatalogAttributeKey,
                Label = attr.Label,
                DataType = attr.AttributeDataType?.Name ?? "String",
                IsCustom = attr.IsCustom,
                IsRequired = attr.IsRequired,
                IsFilterable = attr.IsFilterable,
                IsSummary = attr.IsSummary,
                SortOrder = attr.SortOrder,
                AllowedValues = attr.AllowedValues != null
                            ? attr.AllowedValues
                                .OrderBy(v => v.SortOrder)
                                .Select(v => new CatalogAttributeAllowedValueDto
                                {
                                    Id = v.Id,
                                    Value = v.Value,
                                    SortOrder = v.SortOrder
                                })
                                .ToList()
                            : new List<CatalogAttributeAllowedValueDto>(),
                Icon = attr.AttributeIcon != null
                            ? new CatalogAttributeIconDto
                            {
                                IconCodePoint = attr.AttributeIcon.IconCodePoint,
                                IconFontFamily = attr.AttributeIcon.IconFontFamily,
                                IconName = attr.AttributeIcon.IconName,
                            }
                            : null
            }).OrderBy(a => a.SortOrder).ToList();

            return dtos;

        }

        public async Task<IEnumerable<CatalogAttributeGroupDto>> GetGroupedAttributesAsync(int categoryId, int subCategoryId, bool summaryOnly = false)
        {
            var attributes = await attributeRepository
                .GetAttributesByCategoryOrSubCategoryAsync(categoryId, subCategoryId, summaryOnly);

            var grouped = attributes
                .Select(g => new CatalogAttributeGroupDto
                {
                    //GroupName = g.Key,
                    //Attributes = g.Select(attr => new CatalogAttributeDto
                    //{
                    //    Id = attr.Id,
                    //    Key = attr.CatalogAttributeKey,
                    //    Label = attr.Label,
                    //    DataType = attr.AttributeDataType?.Name ?? "String",
                    //    IsCustom = attr.IsCustom,
                    //    IsRequired = attr.IsRequired,
                    //    IsFilterable = attr.IsFilterable,
                    //    IsSummary = attr.IsSummary,
                    //    SortOrder = attr.SortOrder,
                    //    AllowedValues = attr.AllowedValues != null
                    //        ? attr.AllowedValues
                    //            .OrderBy(v => v.SortOrder)
                    //            .Select(v => new CatalogAttributeAllowedValueDto
                    //            {
                    //                Id = v.Id,
                    //                Value = v.Value,
                    //                SortOrder = v.SortOrder
                    //            })
                    //            .ToList()
                    //        : new List<CatalogAttributeAllowedValueDto>(),
                    //    Icon = attr.AttributeIcon != null
                    //        ? new CatalogAttributeIconDto
                    //        {
                    //            IconCodePoint = attr.AttributeIcon.IconCodePoint,
                    //            IconFontFamily = attr.AttributeIcon.IconFontFamily,
                    //            IconName = attr.AttributeIcon.IconName,
                    //        }
                    //        : null
                    //}).OrderBy(a => a.SortOrder).ToList()
                })
                .OrderBy(g => g.GroupName)
                .ToList();

            return grouped;

        }

        public async Task<FilterAttributeGroupDto> GetFilterableAttributes(int scid)
        {
            try
            {
                var groupedAttributes = await attributeRepository.GetFilterableAttributes(scid);

                var first = groupedAttributes.FirstOrDefault();

                var attributeGroup = new FilterAttributeGroupDto
                {
                    Cid = first?.CategoryMasterId ?? 0,
                    Scid = first?.SubCategoryMasterId ?? 0,
                    Attributes = groupedAttributes
                        .GroupBy(r => r.Id)
                        .Select(g =>
                        {
                            var item = g.First();
                            return new FilterableAttributeDto
                            {
                                Id = g.Key,
                                Key = item.Key,
                                Label = item.Label,
                                AllowedValues = g
                                    .Where(x => !string.IsNullOrEmpty(x.AllowedValue))
                                    .OrderBy(x => x.AllowedValueSortOrder)
                                    .Select(v => new CatalogAttributeAllowedValueDto
                                    {
                                        Id = v.AllowedValueId ?? 0,
                                        Value = v.AllowedValue ?? "",
                                        SortOrder = v.AllowedValueSortOrder ?? 0
                                    })
                                    .ToList()
                            };
                        })
                        .ToList()
                };

                return attributeGroup;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching filterable attributes.");

                return new FilterAttributeGroupDto();
            }
        }

    }
}
