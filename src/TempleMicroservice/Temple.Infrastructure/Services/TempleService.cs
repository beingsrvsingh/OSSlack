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

        public async Task<TempleMaster?> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await _templeRepository.GetByIdWithDetailsAsync(id);
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

        public async Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var (products, totalCount) = await _templeRepository.SearchAsync(query, page, pageSize, cancellationToken);

                var resultDtos = products.Select(p => new SearchItemDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Description = p.Description ?? "",
                    Price = (double)(p.Price ?? 0),
                    ThumbnailUrl = p.ThumbnailUrl ?? "",
                    Score = p.Score,
                    MatchType = p.MatchType ?? "Partial",
                    CategoryId = p.CategoryId,
                    SubcategoryId = p.SubcategoryId
                }).ToList();

                var normalizedQuery = query.Trim();

                bool isCatOrSubcatExact = products.Any(p =>
                    string.Equals(p.CatSnap?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(p.SubcatSnap?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                bool isNameExact = products.Any(p =>
                    string.Equals(p.Name?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                string matchType = isCatOrSubcatExact || isNameExact ? "Exact" : "Partial";

                bool enableFilters = isCatOrSubcatExact || products.Any(p =>
                (p.CatSnap?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.SubcatSnap?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false));

                return new SearchResultDto
                {
                    Results = resultDtos,
                    TotalCount = totalCount,
                    HasMoreResults = page * pageSize < totalCount,
                    Score = products.FirstOrDefault()?.Score ?? 0,
                    MatchType = matchType,
                    EnableFilters = enableFilters,
                    Source = "Temple"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Query: '{Query}', Page: {Page}, PageSize: {PageSize}", query, page, pageSize);
                return new SearchResultDto
                {
                    Results = new List<SearchItemDto>(),
                    TotalCount = 0,
                    HasMoreResults = false,
                    Score = 0,
                    MatchType = "Partial",
                    EnableFilters = false,
                    Source = "Temple"
                };
            }
        }
    }

}
