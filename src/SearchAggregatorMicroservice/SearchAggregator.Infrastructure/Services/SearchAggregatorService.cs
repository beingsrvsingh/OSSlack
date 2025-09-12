using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SearchAggregatorService> _logger;

        private const float ExactMatchThreshold = 0.9f;

        public SearchAggregatorService(
            IProductClient productClient,
            IPriestClient priestClient,
            IAstrologerClient astrologerClient,
            ITempleClient templeClient,
            IKathavachakClient kathavachakClient,
            ILogger<SearchAggregatorService> logger)
        {
            _productClient = productClient;
            _priestClient = priestClient;
            _templeClient = templeClient;
            _astrologerClient = astrologerClient;
            _kathavachakClient = kathavachakClient;
            _logger = logger;
        }

        public async Task<SearchResponse> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            var aggregatedResults = new List<AggregatedSearchResultDto>();
            bool directMatchFound = false;

            // Step 1: Try all MSs in parallel for exact match
            var productTask = SafeProductSearchAsync(query, page, pageSize, cancellationToken);
            var priestTask = SafePriestSearchAsync(query, page, pageSize, cancellationToken);
            var astrologerTask = SafeAstrologerSearchAsync(query, page, pageSize, cancellationToken);
            var templeTask = SafeTempleSearchAsync(query, page, pageSize, cancellationToken);
            var kathavachakTask = SafeKathavachakSearchAsync(query, page, pageSize, cancellationToken);

            await Task.WhenAll(productTask, priestTask, astrologerTask, templeTask);

            var results = new[] { productTask.Result, priestTask.Result, astrologerTask.Result, templeTask.Result, kathavachakTask.Result };

            foreach (var result in results)
            {
                if (HasExactMatch(result))
                {
                    directMatchFound = true;
                    aggregatedResults.AddRange(MapToSearchResult(result));
                }
            }

            // Step 2: If no exact match, include all partial results
            if (!directMatchFound)
            {
                foreach (var result in results)
                {
                    if (result != null)
                        aggregatedResults.AddRange(MapToSearchResult(result));
                }
            }

            // Step 3: Order and paginate
            var orderedResults = aggregatedResults
                .OrderByDescending(r => r.Score)
                .ToList();

            var pagedResults = orderedResults
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Step 4: Filter metadata
            bool enableFilter = aggregatedResults.Any(r => r.EnableFilter);
            int? categoryId = enableFilter ? aggregatedResults.FirstOrDefault(r => r.CategoryId != 0)?.CategoryId ?? 0 : null;
            int? subcategoryId = enableFilter ? aggregatedResults.FirstOrDefault(r => r.SubcategoryId != 0)?.SubcategoryId ?? 0 : null;

            return new SearchResponse
            {
                IsDirectMatch = directMatchFound,
                Results = pagedResults,
                Page = page,
                PageSize = pageSize,
                TotalCount = aggregatedResults.Count,
                Filters = new FilterMetadata
                {
                    EnableFilter = enableFilter,
                    CategoryId = categoryId,
                    SubcategoryId = subcategoryId
                }
            };

        }

        // Wraps Product MS call safely
        private async Task<SearchResultDto?> SafeProductSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productClient.SearchAsync(query, page, pageSize, cancellationToken);
                if (result != null) result.Source = "Product";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product search failed");
                return null;
            }
        }

        private async Task<SearchResultDto?> SafePriestSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _priestClient.SearchAsync(query, page, pageSize, cancellationToken);
                if (result != null) result.Source = "Priest";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Priest search failed");
                return null;
            }
        }

        private async Task<SearchResultDto?> SafeAstrologerSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _astrologerClient.SearchAsync(query, page, pageSize, cancellationToken);
                if (result != null) result.Source = "Astrologer";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Astrologer search failed");
                return null;
            }
        }

        private async Task<SearchResultDto?> SafeTempleSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _templeClient.SearchAsync(query, page, pageSize, cancellationToken);
                if (result != null) result.Source = "Temple";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Temple search failed");
                return null;
            }
        }

        private async Task<SearchResultDto?> SafeKathavachakSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _kathavachakClient.SearchAsync(query, page, pageSize, cancellationToken);
                if (result != null) result.Source = "Kathavachak";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Temple search failed");
                return null;
            }
        }

        // Maps SearchResultDto to List<AggregatedSearchResultDto>
        private List<AggregatedSearchResultDto> MapToSearchResult(SearchResultDto? searchResult)
        {
            if (searchResult == null || searchResult.Results == null)
                return new List<AggregatedSearchResultDto>();

            return searchResult.Results.Select(r => new AggregatedSearchResultDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                ThumbnailUrl = r.ThumbnailUrl,
                Price = r.Price,
                MatchType = r.MatchType ?? GetMatchTypeLabel(r.Score),
                Source = searchResult.Source,
                Score = r.Score,
                CategoryId = r.CategoryId,
                SubcategoryId = r.SubcategoryId,
                EnableFilter = searchResult.EnableFilters
            }).ToList();
        }

        private string GetMatchTypeLabel(float score)
        {
            if (score >= 0.9f) return "Exact";
            if (score >= 0.7f) return "Highly Relevant";
            if (score >= 0.4f) return "Relevant";
            return "Partial";
        }

        // Checks if SearchResultDto contains any exact match based on MatchType and Score in results
        private bool HasExactMatch(SearchResultDto? result)
        {
            if (result == null || result.Results == null || !result.Results.Any())
                return false;

            // If any item is Exact match with score above threshold
            return result.Results.Any(r =>
                !string.IsNullOrEmpty(r.MatchType) &&
                r.MatchType.Equals("Exact", StringComparison.OrdinalIgnoreCase) &&
                r.Score >= ExactMatchThreshold);
        }
    }
}
