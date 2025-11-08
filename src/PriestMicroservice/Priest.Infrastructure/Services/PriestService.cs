using Microsoft.EntityFrameworkCore;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;

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

        public async Task<CatalogResponseDto?> GetPriestByIdAsync(int id)
        {
            _logger.LogInfo($"Getting astrologer by Id: {id}");
            try
            {
                var query = _priestRepository.Query();

                var priest = await query
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
                        Media = p.AstrologerMedia.Select(img => new MediaResponseDto
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
                        Variants = p.PriestExpertise.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Price = v.Price,
                            MRP = v.MRP,
                            StockQuantity = v.StockQuantity,
                            DurationMinutes = v.DurationMinutes,
                            Attributes = v.AttributeValues.Select(a => new AttributeResponseDto
                            {
                                Label = a.AttributeLabel ?? "",
                                Value = a.Value,
                                DataTypeId = a.AttributeDataTypeId,
                            }).ToList(),
                            Addons = v.Addons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Price = a.Price,
                                Description = a.Description,
                                Currency = a.Currency ?? "0"
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

                if (priest == null)
                    _logger.LogWarning($"Astrologer with Id {id} not found.");
                return priest;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with Id {id}", ex);
                throw;
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
