using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Entities;
using Temple.Domain.Repositories;

namespace Temple.Infrastructure.Services
{
    public class TempleService : ITempleService
    {
        private readonly ITempleRepository _templeRepository;
        private readonly ILoggerService<TempleService> _logger;

        public TempleService(ITempleRepository templeRepository, ILoggerService<TempleService> logger)
        {
            _templeRepository = templeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _templeRepository.GetAllAsync(page, pageSize);
        }

        public async Task<CatalogResponseDto?> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                var query = _templeRepository.Query();

                var productDto = await query
                    .Where(p => p.Id == id)
                    .Select(p => new CatalogResponseDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ThumbnailUrl = p.ThumbnailUrl,
                        IsActive = p.IsActive,
                        Rating = p.Rating,
                        Reviews = p.Reviews,
                        CategoryId = p.CategoryId,
                        SubCategoryId = p.SubCategoryId,
                        CategoryName = p.CategoryNameSnapshot,
                        SubCategoryName = p.SubCategoryNameSnapshot,
                        Currency = p.Currency ?? "INR",
                        IsTrending = p.IsTrending,
                        IsFeatured = p.IsFeatured,

                        // Media
                        Media = p.TempleImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Product-level addons
                        Addons = p.TempleAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Price = a.Price,
                            Description = a.Description,
                            Currency = a.Currency ?? "0"
                        }).ToList(),

                        // Product-level attributes
                        Attributes = p.TempleAttributes.Select(a => new AttributeResponseDto
                        {
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId,
                        }).ToList(),

                        // Variants
                        Variants = p.TempleExpertises.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Price = v.Price,
                            MRP = v.MRP,
                            StockQuantity = v.StockQuantity,
                            DurationMinutes = v.DurationMinutes,
                            BookingType = v.BookingType,
                            AvailableSlots = v.AvailableSlots,
                            Attributes = v.AttributeValues.Select(a => new AttributeResponseDto
                            {
                                Label = a.AttributeLabel ?? "",
                                Value = a.Value,
                                DataTypeId = a.AttributeDataTypeId,
                            }).ToList(),
                            Addons = v.TempleAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Price = a.Price,
                                Description = a.Description,
                                Currency = a.Currency ?? "0"
                            }).ToList(),
                            Media = v.TempleExpertiseImages.Select(img => new MediaResponseDto
                            {
                                Url = img.ImageUrl,
                                Type = img.MediaType.ToString(),
                                AltText = img.AltText,
                                SortOrder = img.SortOrder
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return productDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByIdWithDetailsAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<bool> CreateAsync(TempleMaster temple)
        {
            try
            {
                await _templeRepository.AddAsync(temple);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleMaster temple)
        {
            try
            {
                await _templeRepository.UpdateAsync(temple);
                await _templeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _templeRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _templeRepository.DeleteAsync(entity);
                await _templeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var (products, totalCount) = await _templeRepository.SearchAsync(query, page, pageSize, cancellationToken);

                var resultDtos = products.Select(p => new SearchItemDto
                {
                    Pid = p.Id.ToString(),
                    CategoryId = p.CategoryId.ToString(),
                    SubCategoryId = p.SubcategoryId.ToString(),
                    Name = p.Name ?? "",
                    //Cost = (double)(p.Price ?? 0),
                    ThumbnailUrl = p.ThumbnailUrl ?? "",
                    //CategoryType = "Temple",
                    //Quantity = 1,
                    //Limit = 1,
                    Rating = 1,
                    Reviews = 10,
                    AttributeValues = p.AttributeValues ?? [],
                    SearchItemMeta = new SearchItemMeta
                    {
                        Score = p.Score,
                        MatchType = p.MatchType ?? "Partial",
                    }
                }).ToList();

                var normalizedQuery = query.Trim();

                bool isCatOrSubcatExact = products.Any(p =>
                    string.Equals(p.CategoryNameSnapshot?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(p.SubCategoryNameSnapshot?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                bool isNameExact = products.Any(p =>
                    string.Equals(p.Name?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                string matchType = isCatOrSubcatExact || isNameExact ? "Exact" : "Partial";

                bool enableFilters = isCatOrSubcatExact || products.Any(p =>
                (p.CategoryNameSnapshot?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.SubCategoryNameSnapshot?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false));

                var filterMeta = new BaseSearchFilterMetadata
                {
                    TotalCount = totalCount,
                    HasMoreResults = page * pageSize < totalCount,
                    Score = products.FirstOrDefault()?.Score ?? 0,
                    MatchType = matchType,
                    EnableFilters = enableFilters,
                    Source = "Temple"
                };

                var result = new ProductSearchRawResultDto()
                {
                    Results = resultDtos,
                    Filters = filterMeta
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Query: '{Query}', Page: {Page}, PageSize: {PageSize}", query, page, pageSize);
                return new ProductSearchRawResultDto
                {
                    Results = new List<SearchItemDto>(),
                    Filters = new BaseSearchFilterMetadata
                    {
                        TotalCount = 0,
                        HasMoreResults = false,
                        Score = 0,
                        MatchType = "Partial",
                        EnableFilters = false,
                        Source = "Product"
                    }
                };
            }
        }
    }

}
