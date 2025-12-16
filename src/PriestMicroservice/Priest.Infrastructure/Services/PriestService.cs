using Microsoft.EntityFrameworkCore;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;

namespace Priest.Infrastructure.Services
{
    public class PriestService : IPriestService
    {
        private readonly IPriestRepository _priestRepository;
        //private readonly IConsultationModeRepository _consultationModeRepository;
        //private readonly IPriestExpertiseRepository _expertiseRepository;
        //private readonly IPriestLanguageRepository _languageRepository;
        //private readonly IScheduleRepository _scheduleRepository;
        //private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly ILoggerService<PriestService> _logger;

        public PriestService(
            IPriestRepository priestRepository,
            //IConsultationModeRepository consultationModeRepository,
            //IPriestExpertiseRepository expertiseRepository,
            //IPriestLanguageRepository languageRepository,
            //IScheduleRepository scheduleRepository,
            //ITimeSlotRepository timeSlotRepository,
            ILoggerService<PriestService> logger)
        {
            _priestRepository = priestRepository;
            //_consultationModeRepository = consultationModeRepository;
            //_expertiseRepository = expertiseRepository;
            //_languageRepository = languageRepository;
            //_scheduleRepository = scheduleRepository;
            //_timeSlotRepository = timeSlotRepository;
            _logger = logger;
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5)
        {
            List<PriestMaster> lstProducts = new List<PriestMaster>();

            lstProducts = (List<PriestMaster>)await _priestRepository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

            var trendingProducts = lstProducts
                                    .Take(topN)
                                    .Select(product =>
                                    {
                                        return new TrendingResponse
                                        {
                                            Id = product.Id.ToString(),
                                            Scid = product.SubCategoryId.ToString(),
                                            Name = product.Name
                                        };
                                    })
                                    .ToList();


            return trendingProducts;
        }

        public async Task<List<CatalogResponseDto>?> GetPriestsBySubCategoryIdAsync(int? subCategoryId = null, int topN = 5)
        {
            try
            {
                // Use IQueryable from repository
                var query = _priestRepository.Query();

                if (subCategoryId.HasValue && subCategoryId.Value > 0)
                {
                    query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
                }

                var products = await query
                    .Take(topN)
                    .Select(p => new CatalogResponseDto
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name,
                        ThumbnailUrl = p.ThumbnailUrl,
                        Rating = p.Rating,
                        Reviews = p.Reviews,
                        SubCategoryId = p.SubCategoryId.ToString(),
                        IsTrending = p.IsTrending,
                        IsFeatured = p.IsFeatured,
                        Price = new PriceResponseDto
                        {
                            Amount = p.Price.Amount,
                            Currency = p.Price.Currency,
                            Discount = p.Price.Discount,
                            Mrp = p.Price.Mrp,
                            Tax = p.Price.Tax
                        },

                        // Media
                        Media = p.PriestMedias.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // addons
                        Addons = p.Addons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = p.Price.Amount,
                                Mrp = p.Price.Mrp
                            },
                        }).ToList(),

                        // attributes
                        Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                        }).ToList(),

                        // Variants
                        Variants = p.PriestExpertise.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id.ToString(),
                            Name = v.Name,
                            Price = new PriceResponseDto
                            {
                                Amount = v.Price.Amount,
                                Currency = v.Price.Currency,
                                Discount = v.Price.Discount,
                                Mrp = v.Price.Mrp,
                                Tax = v.Price.Tax
                            },
                            StockQuantity = v.StockQuantity,
                            Attributes = v.AttributeValues.AsEnumerable()
                                .GroupBy(a => a.AttributeGroupNameSnapshot)
                                .Select(g => new AttributeGroupResponseDto
                                {
                                    AttributeGroupName = g.Key,
                                    Attributes = g.Select(a => new AttributeResponseDto
                                    {
                                        Key = a.AttributeKey,
                                        Label = a.AttributeLabel ?? "",
                                        Value = a.Value,
                                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                    }).ToList()
                                })
                                .ToList(),
                            Addons = v.Addons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                },
                            }).ToList(),
                            Media = v.PriestExpertiseMedia.Select(img => new MediaResponseDto
                            {
                                Url = img.ImageUrl,
                                Type = img.MediaType.ToString(),
                                AltText = img.AltText,
                                SortOrder = img.SortOrder
                            }).ToList()
                        }).ToList()
                    }).ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetProductBySubCategoryIdAsync");
                return new List<CatalogResponseDto>();
            }
        }

        public async Task<List<CatalogResponseDto>> GetFilteredAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10)
        {
            var query = _priestRepository.Query();

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (attributeIds != null && attributeIds.Any())
            {
                // Ensure product has all selected attribute IDs
                query = query.Where(p => attributeIds.All(attrId =>
                    p.AttributeValues.Any(av => av.CatalogAttributeValueId == attrId)));
            }

            // Take top N products
            var products = await query
                .Take(topN)
                .Select(p => new CatalogResponseDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ThumbnailUrl = p.ThumbnailUrl,
                    Rating = p.Rating,
                    Reviews = p.Reviews,
                    SubCategoryId = p.SubCategoryId.ToString(),
                    IsTrending = p.IsTrending,
                    IsFeatured = p.IsFeatured,
                    Price = new PriceResponseDto
                    {
                        Amount = p.Price.Amount,
                        Currency = p.Price.Currency,
                        Discount = p.Price.Discount,
                        Mrp = p.Price.Mrp,
                        Tax = p.Price.Tax
                    },

                    // Media
                    Media = p.PriestMedias.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Addons
                    Addons = p.Addons.Select(a => new AddonResponseDto
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Price = new PriceResponseDto
                        {
                            Amount = a.Price.Amount,
                            Mrp = a.Price.Mrp
                        }
                    }).ToList(),

                    // Attributes
                    Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                    }).ToList(),

                    // Variants
                    Variants = p.PriestExpertise.Select(v => new CatalogVariantResponseDto
                    {
                        Id = v.Id.ToString(),
                        Name = v.Name,
                        Price = new PriceResponseDto
                        {
                            Amount = v.Price.Amount,
                            Currency = v.Price.Currency,
                            Discount = v.Price.Discount,
                            Mrp = v.Price.Mrp,
                            Tax = v.Price.Tax
                        },
                        StockQuantity = v.StockQuantity,
                        Attributes = v.AttributeValues
                            .GroupBy(a => a.AttributeGroupNameSnapshot)
                            .Select(g => new AttributeGroupResponseDto
                            {
                                AttributeGroupName = g.Key,
                                Attributes = g.Select(a => new AttributeResponseDto
                                {
                                    Key = a.AttributeKey,
                                    Label = a.AttributeLabel ?? "",
                                    Value = a.Value,
                                    DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                }).ToList()
                            }).ToList(),
                        Addons = v.Addons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            }
                        }).ToList(),
                        Media = v.PriestExpertiseMedia.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return products;
        }

        public async Task<CatalogResponseDto?> GetPriestByIdAsync(int id)
        {
            _logger.LogInfo($"Getting astrologer by Id: {id}");
            try
            {
                var query = _priestRepository.Query();

                var productDto = await query
                .Where(p => p.Id == id)
                .Select(p => new CatalogResponseDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ThumbnailUrl = p.ThumbnailUrl,
                    Rating = p.Rating,
                    Reviews = p.Reviews,
                    SubCategoryId = p.SubCategoryId.ToString(),
                    Price = new PriceResponseDto
                    {
                        Amount = p.Price.Amount,
                        Currency = p.Price!.Currency,
                        Discount = p.Price.Discount,
                        Mrp = p.Price.Mrp,
                        Tax = p.Price.Tax
                    },
                    IsTrending = p.IsTrending,
                    IsFeatured = p.IsFeatured,

                    // Media
                    Media = p.PriestMedias.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Product-level addons
                    Addons = p.Addons.Select(a => new AddonResponseDto
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Price = new PriceResponseDto
                        {
                            Amount = a.Price.Amount,
                            Mrp = a.Price.Mrp
                        },
                    }).ToList(),

                    // Product-level attributes
                    Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                    }).ToList(),

                    // Variants
                    Variants = p.PriestExpertise.Select(v => new CatalogVariantResponseDto
                    {
                        Id = v.Id.ToString(),
                        Name = v.Name,
                        Price = new PriceResponseDto
                        {
                            Amount = v.Price.Amount,
                            Currency = p.Price!.Currency,
                            Discount = p.Price.Discount,
                            Mrp = p.Price.Mrp,
                            Tax = p.Price.Tax
                        },
                        StockQuantity = v.StockQuantity,
                        Attributes = v.AttributeValues.AsEnumerable()
                                .GroupBy(a => a.AttributeGroupNameSnapshot)
                                .Select(g => new AttributeGroupResponseDto
                                {
                                    AttributeGroupName = g.Key,
                                    Attributes = g.Select(a => new AttributeResponseDto
                                    {
                                        Key = a.AttributeKey,
                                        Label = a.AttributeLabel ?? "",
                                        Value = a.Value,
                                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                    }).ToList()
                                })
                                .ToList(),
                        Addons = v.Addons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            },
                        }).ToList(),
                        Media = v.PriestExpertiseMedia.Select(img => new MediaResponseDto
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

        public async Task<IEnumerable<PriestMaster>> GetAllActivePriestsAsync()
        {
            try
            {
                return await _priestRepository.GetAsync(p => p.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch active priests.");
                return Enumerable.Empty<PriestMaster>();
            }
        }

        public async Task AddPriestAsync(PriestMaster priest)
        {
            try
            {
                await _priestRepository.AddAsync(priest);
                await _priestRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add new priest.");
            }
        }

        public async Task UpdatePriestAsync(PriestMaster priest)
        {
            try
            {
                await _priestRepository.UpdateAsync(priest);
                await _priestRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update priest with ID: {priest.Id}");
            }
        }

        public async Task DeletePriestAsync(int id)
        {
            try
            {
                var priest = await _priestRepository.GetByIdAsync(id);
                if (priest == null)
                {
                    _logger.LogError($"Priest with ID {id} not found.");
                    return;
                }

                await _priestRepository.DeleteAsync(priest);
                await _priestRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete priest with ID: {id}");
            }
        }

        public async Task<IEnumerable<ConsultationMode>> GetConsultationModesByPriestIdAsync(int expertiseId)
        {
            try
            {
                return [];// await _consultationModeRepository.GetAsync(cm => cm.ExpertiseId == expertiseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch consultation modes for priest ID: {expertiseId}");
                return Enumerable.Empty<ConsultationMode>();
            }
        }

        public async Task<IEnumerable<PriestExpertise>> GetExpertiseByPriestIdAsync(int priestId)
        {
            try
            {
                return [];// await _expertiseRepository.GetAsync(e => e.PriestId == priestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch expertise for priest ID: {priestId}");
                return Enumerable.Empty<PriestExpertise>();
            }
        }

        public async Task<IEnumerable<PriestLanguage>> GetLanguagesByPriestIdAsync(int priestId)
        {
            try
            {
                return [];// await _languageRepository.GetAsync(l => l.PriestId == priestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch languages for priest ID: {priestId}");
                return Enumerable.Empty<PriestLanguage>();
            }
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByPriestIdAsync(int priestId)
        {
            try
            {
                return [];// await _scheduleRepository.GetAsync(s => s.PriestId == priestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch schedules for priest ID: {priestId}");
                return Enumerable.Empty<Schedule>();
            }
        }

        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByScheduleIdAsync(int scheduleId)
        {
            try
            {
                return [];// await _timeSlotRepository.GetAsync(t => t.ScheduleId == scheduleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch time slots for schedule ID: {scheduleId}");
                return Enumerable.Empty<TimeSlot>();
            }
        }

        public async Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var (products, totalCount) = await _priestRepository.SearchAsync(query, page, pageSize, cancellationToken);

                var resultDtos = products.Select(p => new SearchItemDto
                {
                    Pid = p.Id.ToString(),
                    CategoryId = p.CategoryId.ToString(),
                    SubCategoryId = p.SubcategoryId.ToString(),
                    Name = p.Name ?? "",
                    Price = (double)(p.Price ?? 0),
                    ThumbnailUrl = p.ThumbnailUrl ?? "",
                    Quantity = 1,
                    Limit = 1,
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

                //bool isCatOrSubcatExact = products.Any(p =>
                //    string.Equals(p.CategoryNameSnapshot?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase)
                //    || string.Equals(p.SubCategoryNameSnapshot?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                bool isNameExact = products.Any(p =>
                    string.Equals(p.Name?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                string matchType = isNameExact ? "Exact" : "Partial";
                bool enableFilters = matchType == "Exact" ? true : false;

                //bool enableFilters = isCatOrSubcatExact || products.Any(p =>
                //(p.CategoryNameSnapshot?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                //(p.SubCategoryNameSnapshot?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false));

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
