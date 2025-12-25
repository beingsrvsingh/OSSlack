using Azure;
using Azure.Core;
using Microsoft.Extensions.Logging;
using NLog.Filters;
using SearchAggregator.Application.Clients;
using SearchAggregator.Application.Contracts;
using SearchAggregator.Application.Contracts.Dtos;
using SearchAggregator.Application.Services;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

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

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var allResults = new List<CatalogResponseDto>();
            bool directMatchFound = false;

            // Start all searches in parallel
            var productTask = SafeProductSearchAsync(query, pageNumber, pageSize, cancellationToken);
            var priestTask = SafePriestSearchAsync(query, pageNumber, pageSize, cancellationToken);
            var astrologerTask = SafeAstrologerSearchAsync(query, pageNumber, pageSize, cancellationToken);
            var templeTask = SafeTempleSearchAsync(query, pageNumber, pageSize, cancellationToken);
            var kathavachakTask = SafeKathavachakSearchAsync(query, pageNumber, pageSize, cancellationToken);
            var poojaTask = SafePoojaSearchAsync(query, pageNumber, pageSize, cancellationToken);

            await Task.WhenAll(
                productTask,
                priestTask,
                astrologerTask,
                templeTask,
                kathavachakTask,
                poojaTask
                );

            // Filter out null results
            var sources = new[]
            {
                productTask.Result,
                priestTask.Result,
                astrologerTask.Result,
                templeTask.Result,
                kathavachakTask.Result,
                poojaTask.Result
            }.Where(result => result != null).ToList();


            var unifiedResult = new PagedResult<CatalogResponseDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,

                // Total from all MS
                TotalCount = sources.Sum(r => r.TotalCount),

                // Merge all items
                Items = sources
                        .SelectMany(r => r.Items)
                        .ToList()
            };


            return unifiedResult;
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
        private async Task<PagedResult<CatalogResponseDto>?> SafeProductSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        private async Task<PagedResult<CatalogResponseDto>?> SafePriestSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _priestClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Priest search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        private async Task<PagedResult<CatalogResponseDto>?> SafeAstrologerSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _astrologerClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Astrologer search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        private async Task<PagedResult<CatalogResponseDto>?> SafeTempleSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _templeClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Temple search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        private async Task<PagedResult<CatalogResponseDto>?> SafeKathavachakSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _kathavachakClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kathavachak search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        private async Task<PagedResult<CatalogResponseDto>?> SafePoojaSearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _poojaClient.SearchAsync(query, page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Pooja search failed");
                return new PagedResult<CatalogResponseDto>();
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