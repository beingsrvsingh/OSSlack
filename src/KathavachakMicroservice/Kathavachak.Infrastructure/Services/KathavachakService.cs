using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakService : IKathavachakService
    {
        private readonly IKathavachakRepository _repository;
        private readonly ILoggerService<KathavachakService> _logger;

        public KathavachakService(IKathavachakRepository repository, ILoggerService<KathavachakService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CatalogResponseDto?> GetByIdAsync(int id)
        {
            _logger.LogInfo($"Getting kathavachak by Id: {id}");
            try
            {
                var query = _repository.Query();

                var kathavachak = await query
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
                        Media = p.KathavachakMedia.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Astrologer-level addons
                        Addons = p.KathavachakAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Price = a.Price,
                            Description = a.Description,
                            Currency = a.Currency ?? "0"
                        }).ToList(),

                        // Astrologer-level attributes
                        Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                        {
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId,
                        }).ToList(),

                        // Variants
                        Variants = p.KathavachakExpertises.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Price = v.Price,
                            MRP = v.MRP,
                            StockQuantity = v.StockQuantity,
                            DurationMinutes = v.DurationMinutes,
                            Attributes = v.KathavachakAttributeValues.Select(a => new AttributeResponseDto
                            {
                                Label = a.AttributeLabel ?? "",
                                Value = a.Value,
                                DataTypeId = a.AttributeDataTypeId,
                            }).ToList(),
                            Addons = v.KathavachakAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Price = a.Price,
                                Description = a.Description,
                                Currency = a.Currency ?? "0"
                            }).ToList(),
                            Media = v.KathavachakExpertiseMedia.Select(img => new MediaResponseDto
                            {
                                Url = img.ImageUrl,
                                Type = img.MediaType.ToString(),
                                AltText = img.AltText,
                                SortOrder = img.SortOrder
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (kathavachak == null)
                    _logger.LogWarning($"Astrologer with Id {id} not found.");
                return kathavachak;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with Id {id}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<KathavachakMaster>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAsync: {ex.Message}", ex);
                return Enumerable.Empty<KathavachakMaster>();
            }
        }

        public async Task<bool> CreateAsync(KathavachakMaster entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(KathavachakMaster entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync();
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
                var kathavachak = await _repository.GetByIdAsync(id);
                if(kathavachak == null)
                {
                    _logger.LogWarning($"Kathavachak with ID {id} not found for deletion.");
                    return false;
                }
                kathavachak.IsActive = false;
                await _repository.UpdateAsync(kathavachak);
                await _repository.SaveChangesAsync();
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
                var (products, totalCount) = await _repository.SearchAsync(query, page, pageSize, cancellationToken);

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
                    Source = "Priest"
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
                        Source = "Priest"
                    }
                };
            }
        }
    }
}
