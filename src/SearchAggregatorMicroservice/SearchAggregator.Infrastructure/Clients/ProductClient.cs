using SearchAggregator.Application.Clients;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace SearchAggregator.Infrastructure.Clients
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<ProductClient> _logger;

        public ProductClient(IHttpClientFactory httpClientFactory, ILoggerService<ProductClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IProductClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {

                var url = $"product/search?q={Uri.EscapeDataString(query)}&page={page}&pageSize={pageSize}";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<CatalogResponseDto>>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? new PagedResult<CatalogResponseDto>();                    
                }

                _logger.LogWarning("Received null response from Product service for query '{Query}'", query);
                return new PagedResult<CatalogResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Product service for query '{Query}'", query);
                throw;
            }
        }
    }
}
