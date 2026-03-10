using Priest.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Priest.Application;
using Priest.Application.Service;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using Priest.Infrastructure.Persistence.Catalog.Queries;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class PriestService : IPriestService
    {
        private readonly IPriestRepository _repository;
        private readonly ILoggerService<PriestService> _logger;
        private readonly IScheduleRepository scheduleRepository;
        private readonly IBookingClient bookingClient;
        private readonly ICatalogService catalogService;

        public PriestService(IPriestRepository priestRepository,ILoggerService<PriestService> logger,IScheduleRepository scheduleRepository,IBookingClient bookingClient, ICatalogService catalogService)
        {
            _repository = priestRepository;
            _logger = logger;
            this.scheduleRepository = scheduleRepository;
            this.bookingClient = bookingClient;
            this.catalogService = catalogService;
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10)
        {
            List<PriestMaster> lstProducts = new List<PriestMaster>();

            lstProducts = (List<PriestMaster>)await _repository.GetAsync((p) => p.PriestExpertises.Any(x => x.CategoryId == subCategoryId));

            var trendingProducts = lstProducts
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .Select(product =>
                                    {
                                        return new TrendingResponse
                                        {
                                            Id = product.Id.ToString(),
                                            Name = product.Name
                                        };
                                    })
                                    .ToList();


            return trendingProducts;
        }


        public async Task<PagedResult<CatalogResponseDto>> GetTrendingProdcutsAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var queryable = _repository.Query();

                var totalCount = await queryable.CountAsync();

                var skip = (pageNumber - 1) * pageSize;

                var products = await queryable
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(pageSize)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .ToListAsync();

                return new PagedResult<CatalogResponseDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    Items = products
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Page: {Page}, PageSize: {PageSize}", pageNumber, pageSize);
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<List<CatalogResponseDto>?> GetPriestsBySubCategoryIdAsync(int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Use IQueryable from repository
                var queryable = _repository.Query();

                if (subCategoryId.HasValue && subCategoryId.Value > 0)
                {
                    queryable = queryable.Where(p => p.PriestExpertises.Any(x => x.SubCategoryId == subCategoryId.Value));
                }

                var products = await queryable
                                    .AsNoTracking()
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .Select(CatalogQueries.ToCatalogResponse)
                                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetProductBySubCategoryIdAsync");
                return new List<CatalogResponseDto>();
            }
        }

        public async Task<List<CatalogResponseDto>> GetFilteredAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            var queryable = _repository.Query();

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                queryable = queryable.Where(p => p.PriestExpertises.Any(x => x.SubCategoryId == subCategoryId.Value));
            }

            //if (attributeIds != null && attributeIds.Any())
            //{
            //    // Ensure product has all selected attribute IDs
            //    queryable = queryable.Where(p => attributeIds.All(attrId =>
            //        p.AttributeValues.Any(av => av.CatalogAttributeValueId == attrId)));
            //}

            var products = await queryable
                                    .AsNoTracking()
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .Select(CatalogQueries.ToCatalogResponse)
                                    .ToListAsync();

            return products;
        }

        public async Task<HubDto> GetPriestByIdAsync(int id)
        {
            _logger.LogInfo($"Getting priest by Id: {id}");

            try
            {
                // Load priest with expertises and consultation modes
                var priestEntity = await _repository.Query()
                    .Where(p => p.Id == id && p.IsActive)
                    .Include(p => p.PriestExpertises)                        
                        .ThenInclude(e => e.ConsultationModes)
                            .ThenInclude(c => c.ConsultationModeMaster)
                     .Include(p => p.PriestExpertises)
                        .ThenInclude(e => e.Addons)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (priestEntity == null)
                    return new HubDto();

                // Collect distinct CategoryIds and SubCategoryIds
                var categoryIds = priestEntity.PriestExpertises.Select(e => e.CategoryId).Distinct().ToList();
                var subCategoryIds = priestEntity.PriestExpertises.Select(e => e.SubCategoryId).Distinct().ToList();

                if (!categoryIds.Any() && !subCategoryIds.Any())
                    return new HubDto();

                // Get category details from catalog service
                IEnumerable<CategoryDetailsResponseDto> categories = await catalogService.GetCategoryDetails(categoryIds, subCategoryIds);

                var todaySchedules = priestEntity.Schedules.Where(s => (DayOfWeek)(s.DayOfWeek - 1) == DateTime.Now.DayOfWeek && s.IsAvailable).ToList();

                var isOpenNow = todaySchedules.Any(s => DateTime.Now.TimeOfDay >= s.StartTime && DateTime.Now.TimeOfDay <= s.EndTime);

                // Map priest data to HubDto
                var hubDto = new HubDto
                {
                    Id = priestEntity.Id,
                    Name = priestEntity.Name,
                    BannerUrl = priestEntity.PriestMedias.Where(m => m.IsThumbnailUrl).Select(m => m.ImageUrl).FirstOrDefault() ?? "https://picsum.photos/200",
                    Rating = priestEntity.Rating,
                    Status = isOpenNow ? "Open now" : "Closed now",
                    WorkingHours = todaySchedules.Any()? string.Join(", ", todaySchedules.Select(s => $"{s.StartTime:hh\\:mm} - {s.EndTime:hh\\:mm}")): "-",
                    Categories = categories.Select(c => new CategoryDto
                    {
                        Id = c.CategoryId,
                        Name = c.CategoryName,
                        Items = priestEntity.PriestExpertises
                        .Where(e => c.SubCategories.Any(sc => sc.SubCategoryId == e.SubCategoryId))
                        .Select(e => new ItemDto
                        {
                            Id = e.ConsultationModes.FirstOrDefault().Id,
                            SubCategoryId = e.SubCategoryId,
                            ExpertiseId = e.Id,
                            Name = c.SubCategories
                                .FirstOrDefault(sc => sc.SubCategoryId == e.SubCategoryId)?.SubCategoryName ?? string.Empty,

                            Price = e.ConsultationModes
                                .Where(m => m.IsDefault)
                                .Select(m => m.Price.Amount)
                                .FirstOrDefault(),

                            Modes = e.ConsultationModes
                                .Select(m => new ConsultationModeDto
                                {
                                    Id = m.Id,
                                    Key = m.ConsultationModeMaster.ModeKey,
                                    DisplayName = m.ConsultationModeMaster.Mode,
                                    Price = m.Price.Amount,
                                    IsDefault = m.IsDefault
                                })
                                .ToList(),

                            AddOns = e.Addons.Select(a => new AddonDto
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Description = a.Description,
                                Price = a.Price.Amount
                            }).ToList(),

                            Availability = e.ConsultationModes.Any(m => m.StockQuantity > 0)
                                ? "available"
                                : "out-of-stock",

                            Description = $"{e.DurationMinutes} mins",

                            ThumbnailUril = c.SubCategories
                                .FirstOrDefault(sc => sc.SubCategoryId == e.SubCategoryId)?.ThumbnailUril ?? string.Empty,

                            HasAddon = e.Addons.Any(),
                            HasModes = e.ConsultationModes.Any(),

                            Metadata = new Dictionary<string, string>
                            {
                                {
                                    "Description",
                                    c.SubCategories.FirstOrDefault(sc => sc.SubCategoryId == e.SubCategoryId)?.Description ?? string.Empty
                                }
                            }
                        })
                        .ToList()
                    })
                    .ToList()
                };

                return hubDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetPriestByIdAsync: {ex.Message}", ex);
                return new HubDto();
            }
        }

        public async Task<IEnumerable<PriestMaster>> GetAllActivePriestsAsync()
        {
            try
            {
                return await _repository.GetAsync(p => p.IsActive);
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
                await _repository.AddAsync(priest);
                await _repository.SaveChangesAsync();
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
                await _repository.UpdateAsync(priest);
                await _repository.SaveChangesAsync();
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
                var priest = await _repository.GetByIdAsync(id);
                if (priest == null)
                {
                    _logger.LogError($"Priest with ID {id} not found.");
                    return;
                }

                await _repository.DeleteAsync(priest);
                await _repository.SaveChangesAsync();
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

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _repository.Query();

                queryable = CatalogQueries.ApplySearch(queryable, query);

                var totalCount = await queryable.CountAsync();

                var skip = (pageNumber - 1) * pageSize;

                var products = await queryable
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(pageSize)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .ToListAsync();

                return new PagedResult<CatalogResponseDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    Items = products
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Query: '{Query}', Page: {Page}, PageSize: {PageSize}", query, pageNumber, pageSize);
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<List<TimeSlotDto>> GetTodayAvailableSlotsAsync(int astrologerId, DateTime date)
        {
            var today = date;
            var todayDay = date.DayOfWeek;

            // Get schedules
            var schedules = await scheduleRepository.GetSchedulesByDayAsync(astrologerId, todayDay);

            if (!schedules.Any())
                return new List<TimeSlotDto>();

            // Check full day exception
            var fullDayBlocked = await scheduleRepository.IsFullDayBlockedAsync(astrologerId, today);

            if (fullDayBlocked)
                return new List<TimeSlotDto>();

            // Get time exceptions
            var exceptions = await scheduleRepository.GetTimeExceptionsAsync(astrologerId, today);

            // Get bookings
            var bookings = await bookingClient.GetBookingsByDateAsync(astrologerId, today);

            var availableSlots = new List<TimeSlotDto>();

            foreach (var schedule in schedules)
            {
                var start = schedule.StartTime;
                var end = schedule.EndTime;

                // Remove blocked exception times
                foreach (var ex in exceptions)
                {
                    if (ex.StartTime.HasValue && ex.EndTime.HasValue)
                    {
                        if (ex.StartTime.Value > start && ex.StartTime.Value < end)
                            end = ex.StartTime.Value;
                    }
                }

                // Remove booking times
                foreach (var booking in bookings)
                {
                    if (booking.StartTime > start && booking.StartTime < end)
                        end = booking.StartTime;
                }

                if (start < end)
                {
                    availableSlots.Add(new TimeSlotDto
                    {
                        StartTime = start,
                        EndTime = end
                    });
                }
            }

            return availableSlots;
        }
    }

}
