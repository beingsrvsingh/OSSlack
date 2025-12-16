using Microsoft.Extensions.Logging;
using NLog.Filters;
using SearchAggregator.Application.Clients;
using SearchAggregator.Application.Contracts;
using SearchAggregator.Application.Contracts.Dtos;
using SearchAggregator.Application.Services;
using Shared.Application.Contracts;

namespace SearchAggregator.Infrastructure.Services
{
    public class SearchAggregatorService : ISearchAggregatorService
    {
        private readonly IProductClient _productClient;
        private readonly IPriestClient _priestClient;
        private readonly IAstrologerClient _astrologerClient;
        private readonly ITempleClient _templeClient;
        private readonly IKathavachakClient _kathavachakClient;
        private readonly IPoojaClient _poojaClient;
        private readonly ILogger<SearchAggregatorService> _logger;

        private const float ExactMatchThreshold = 0.9f;

        public SearchAggregatorService(
            IProductClient productClient,
            IPriestClient priestClient,
            IAstrologerClient astrologerClient,
            ITempleClient templeClient,
            IKathavachakClient kathavachakClient,
            IPoojaClient poojaClient,
            ILogger<SearchAggregatorService> logger)
        {
            _productClient = productClient;
            _priestClient = priestClient;
            _templeClient = templeClient;
            _astrologerClient = astrologerClient;
            _kathavachakClient = kathavachakClient;
            _poojaClient = poojaClient;
            _logger = logger;
        }

        public async Task<SearchResponse> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            var allResults = new List<SearchResponseDto>();
            bool directMatchFound = false;

            // Start all searches in parallel
            var productTask = SafeProductSearchAsync(query, page, pageSize, cancellationToken);
            var priestTask = SafePriestSearchAsync(query, page, pageSize, cancellationToken);
            var astrologerTask = SafeAstrologerSearchAsync(query, page, pageSize, cancellationToken);
            var templeTask = SafeTempleSearchAsync(query, page, pageSize, cancellationToken);
            var kathavachakTask = SafeKathavachakSearchAsync(query, page, pageSize, cancellationToken);
            var poojaTask = SafePoojaSearchAsync(query, page, pageSize, cancellationToken);

            await Task.WhenAll(productTask, priestTask, astrologerTask, templeTask, kathavachakTask);

            // Filter out null results
            var sources = new[]
            {
                productTask.Result,
                priestTask.Result,
                astrologerTask.Result,
                templeTask.Result,
                kathavachakTask.Result
            }.Where(result => result != null).ToList();


            foreach (var sourceResults in sources)
            {
                if (sourceResults == null)
                    continue;

                // Check for direct/exact match based on Filter in individual results
                if (sourceResults.Any(r => IsExactMatch(r)))
                {
                    directMatchFound = true;
                    allResults.AddRange(sourceResults.Where(r => IsExactMatch(r)));
                }
            }

            // If no direct match found, add all results (partial matches)
            if (!directMatchFound)
            {
                foreach (var sourceResults in sources)
                {
                    if (sourceResults != null)
                        allResults.AddRange(sourceResults);
                }
            }

            // Sort by Filter.Score descending, fallback to 0 if missing
            var orderedResults = allResults
                .OrderByDescending(r => r.Filter?.Score ?? 0)
                .ToList();

            // Pagination
            var pagedResults = orderedResults
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Determine if filtering enabled anywhere in results
            bool enableFilter = allResults.Any(r => r.Filter?.EnableFilters == true);

            // Extract first non-zero category and subcategory ids from results
            int? categoryId = enableFilter
                ? allResults.Select(r => int.TryParse(r.CategoryId, out var cid) && cid != 0 ? cid : (int?)null).FirstOrDefault(c => c.HasValue)
                : null;

            int? subcategoryId = enableFilter
                ? allResults.Select(r => int.TryParse(r.SubCategoryId, out var scid) && scid != 0 ? scid : (int?)null).FirstOrDefault(c => c.HasValue)
                : null;

            return new SearchResponse
            {
                Results = pagedResults,
                Filters = new FilterMetadata
                {
                    IsDirectMatch = directMatchFound,
                    Page = page,
                    PageSize = pageSize,
                    TotalCount = allResults.Count,
                    EnableFilter = enableFilter,
                    CategoryId = categoryId,
                    SubcategoryId = subcategoryId
                }
            };
        }

        private bool IsExactMatch(SearchResponseDto item)
        {
            if (item.Filter == null)
                return false;

            return !string.IsNullOrWhiteSpace(item.Filter.MatchType) &&
                   item.Filter.MatchType.Equals("Exact", StringComparison.OrdinalIgnoreCase) &&
                   item.Filter.Score >= ExactMatchThreshold;
        }


        // Safe wrappers for each client
        private async Task<List<SearchResponseDto>?> SafeProductSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product search failed");
                return new List<SearchResponseDto>();
            }
        }

        private async Task<List<SearchResponseDto>?> SafePriestSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _priestClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Priest search failed");
                return new List<SearchResponseDto>();
            }
        }

        private async Task<List<SearchResponseDto>?> SafeAstrologerSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _astrologerClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Astrologer search failed");
                return new List<SearchResponseDto>();
            }
        }

        private async Task<List<SearchResponseDto>?> SafeTempleSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _templeClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Temple search failed");
                return new List<SearchResponseDto>();
            }
        }

        private async Task<List<SearchResponseDto>?> SafeKathavachakSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _kathavachakClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kathavachak search failed");
                return new List<SearchResponseDto>();
            }
        }

        private async Task<List<SearchResponseDto>?> SafePoojaSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _poojaClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kathavachak search failed");
                return new List<SearchResponseDto>();
            }
        }

        private string GetMatchTypeLabel(float score)
        {
            if (score >= 0.9f) return "Exact";
            if (score >= 0.7f) return "Highly Relevant";
            if (score >= 0.4f) return "Relevant";
            return "Partial";
        }
    }
}