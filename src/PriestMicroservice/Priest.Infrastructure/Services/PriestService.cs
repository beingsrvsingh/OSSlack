using Microsoft.EntityFrameworkCore;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using Priest.Infrastructure.Persistence.Catalog.Queries;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Infrastructure.Repositories;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class PriestService : IPriestService
    {
        private readonly IPriestRepository _repository;
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
            _repository = priestRepository;
            //_consultationModeRepository = consultationModeRepository;
            //_expertiseRepository = expertiseRepository;
            //_languageRepository = languageRepository;
            //_scheduleRepository = scheduleRepository;
            //_timeSlotRepository = timeSlotRepository;
            _logger = logger;
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10)
        {
            List<PriestMaster> lstProducts = new List<PriestMaster>();

            lstProducts = (List<PriestMaster>)await _repository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

            var trendingProducts = lstProducts
                                    .Skip(pageNumber)
                                    .Take(pageSize)
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
                                .Where((p) => p.IsTrending == true)
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
                    queryable = queryable.Where(p => p.SubCategoryId == subCategoryId.Value);
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
                queryable = queryable.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (attributeIds != null && attributeIds.Any())
            {
                // Ensure product has all selected attribute IDs
                queryable = queryable.Where(p => attributeIds.All(attrId =>
                    p.AttributeValues.Any(av => av.CatalogAttributeValueId == attrId)));
            }

            var products = await queryable
                                    .AsNoTracking()
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .Select(CatalogQueries.ToCatalogResponse)
                                    .ToListAsync();

            return products;
        }

        public async Task<CatalogResponseDto?> GetPriestByIdAsync(int id)
        {
            _logger.LogInfo($"Getting astrologer by Id: {id}");
            try
            {
                var queryable = _repository.Query();

                var productDto = await queryable
                                .AsNoTracking()
                                .Select(CatalogQueries.ToCatalogResponse)
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
    }

}
