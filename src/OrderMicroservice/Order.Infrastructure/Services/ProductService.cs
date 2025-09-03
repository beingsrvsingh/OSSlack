using System.Net.Http.Json;
using Order.Application.Contracts;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<ProductService> _logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILoggerService<ProductService> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IProductService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ProductSummaryDto>> GetProductsByIdAndCategoryIdAsync(List<int> pids, int cid)
        {
            try
            {
                var url = $"byIdAndCategoryId";

               var response = await _httpClient.PostAsJsonAsync(url, new { pids, cid });

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<ProductSummaryDto>>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? Enumerable.Empty<ProductSummaryDto>();
                }

                return Enumerable.Empty<ProductSummaryDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Product MS for ProductId: ", String.Join(',', pids));
                // or throw if you want to propagate the error
                return Enumerable.Empty<ProductSummaryDto>();
            }
        }
    }
}