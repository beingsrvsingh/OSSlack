using Microsoft.Extensions.Logging;
using SearchAggregator.Application.Clients;
using SearchAggregator.Application.Services;
using Shared.Application.Common.Contracts.Response;
using Shared.Utilities.Response;

namespace SearchAggregator.Infrastructure.Services
{
    public class AggregatorService : IAggregatorService
    {

        private readonly IProductClient _productClient;
        private readonly IPriestClient _priestClient;
        private readonly IAstrologerClient _astrologerClient;
        private readonly ITempleClient _templeClient;
        private readonly IKathavachakClient _kathavachakClient;
        private readonly IPoojaClient _poojaClient;
        private readonly ILogger<AggregatorService> _logger;

        public AggregatorService(
            IProductClient productClient,
            IPriestClient priestClient,
            IAstrologerClient astrologerClient,
            ITempleClient templeClient,
            IKathavachakClient kathavachakClient,
            IPoojaClient poojaClient,
            ILogger<AggregatorService> logger)
        {
            _productClient = productClient;
            _priestClient = priestClient;
            _templeClient = templeClient;
            _astrologerClient = astrologerClient;
            _kathavachakClient = kathavachakClient;
            _poojaClient = poojaClient;
            _logger = logger;
        }

        public async Task<PagedResult<CatalogResponseDto>?> GetTrendingProductAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productClient.GetTrendingProductAsync(page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<PagedResult<CatalogResponseDto>?> GetTrendingPriestAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _priestClient.GetTrendingPriestAsync(page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Priest search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<PagedResult<CatalogResponseDto>?> GetTrendingAstrologerAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _astrologerClient.GetTrendingAstrologerAsync(page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Astrologer search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<PagedResult<CatalogResponseDto>?> GetTrendingTempleAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _templeClient.GetTrendingTempleAsync(page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Temple search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<PagedResult<CatalogResponseDto>?> GetTrendingKathavachakAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _kathavachakClient.GetTrendingKathavachakAsync(page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kathavachak search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<PagedResult<CatalogResponseDto>?> GetTrendingPoojaAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _poojaClient.GetTrendingPoojaAsync(page, pageSize, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Pooja search failed");
                return new PagedResult<CatalogResponseDto>();
            }
        }
    }
}
