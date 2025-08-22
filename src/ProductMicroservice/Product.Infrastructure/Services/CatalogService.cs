
using System.Net.Http.Json;
using Product.Application.Contracts;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<CatalogService> _logger;

        public CatalogService(IHttpClientFactory httpClientFactory, ILoggerService<CatalogService> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(ICatalogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<CatalogAttributeDto>> GetAttributesBySubCategoryIdAsync(int subCategoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"category/{subCategoryId}/attributes");

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<List<CatalogAttributeDto>>>();

                return result?.Data ?? new List<CatalogAttributeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for SubCategoryId: {SubCategoryId}", subCategoryId);
                return new List<CatalogAttributeDto>(); // or throw if required
            }
        }
    }

}