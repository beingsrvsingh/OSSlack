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
        private readonly ILogger<SearchAggregatorService> _logger;

        private const float ExactMatchThreshold = 0.9f;

        public SearchAggregatorService(
            IProductClient productClient,
            IPriestClient priestClient,
            IAstrologerClient astrologerClient,
            ILogger<SearchAggregatorService> logger)
        {
            _productClient = productClient;
            _priestClient = priestClient;
            _astrologerClient = astrologerClient;
            _logger = logger;
        }

        public async Task<SearchResponse> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            var aggregatedResults = new List<AggregatedSearchResultDto>();
            bool directMatchFound = false;

            // Step 1: Try Product MS for exact matches
            var productResult = await SafeProductSearchAsync(query, page, pageSize, cancellationToken);
            if (HasExactMatch(productResult))
            {
                directMatchFound = true;
                aggregatedResults.AddRange(MapToSearchResult(productResult));
            }

            // Step 2: If no direct product exact match, try Priest MS
            if (!directMatchFound)
            {
                var priestResult = await SafePriestSearchAsync(query, page, pageSize, cancellationToken);
                if (HasExactMatch(priestResult))
                {
                    directMatchFound = true;
                    aggregatedResults.AddRange(MapToSearchResult(priestResult));
                }
            }

            // Step 3: If still no exact match, do category/subcategory partial search in all MS concurrently
            if (!directMatchFound)
            {
                var productTask = SafeProductSearchAsync(query, page, pageSize, cancellationToken);
                var priestTask = SafePriestSearchAsync(query, page, pageSize, cancellationToken);
                var astrologerTask = SafeAstrologerSearchAsync(query, page, pageSize, cancellationToken);

                await Task.WhenAll(productTask, priestTask, astrologerTask);

                if (productTask.Result != null)
                    aggregatedResults.AddRange(MapToSearchResult(productTask.Result));
                if (priestTask.Result != null)
                    aggregatedResults.AddRange(MapToSearchResult(priestTask.Result));
                if (astrologerTask.Result != null)
                    aggregatedResults.AddRange(MapToSearchResult(astrologerTask.Result));
            }

            // Order by Score descending (customize if needed)
            var orderedResults = aggregatedResults
                .OrderByDescending(r => r.Score)
                .ToList();

            // Manual pagination after aggregation
            var pagedResults = orderedResults
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Determine FilterMetadata based on aggregatedResults
            var enableFilter = aggregatedResults.Any(r => r.EnableFilter);

            // Extract first non-null category/subcategory for filter metadata if filter is enabled
            int? categoryId = null;
            int? subcategoryId = null;

            if (enableFilter)
            {
                categoryId = aggregatedResults.FirstOrDefault(r => r.CategoryId != 0)?.CategoryId ?? 0;
                subcategoryId = aggregatedResults.FirstOrDefault(r => r.SubcategoryId != 0)?.SubcategoryId ?? 0;
            }

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
