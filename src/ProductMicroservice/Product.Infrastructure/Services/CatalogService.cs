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

        public async Task<IEnumerable<CatalogAttributeGroupDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false)
        {
            try
            {
                var url = $"category/attributes?CategoryId={categoryId}&SubCategoryId={subCategoryId}&IsSummary={isSummary}";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<CatalogAttributeGroupDto>>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? Enumerable.Empty<CatalogAttributeGroupDto>();
                }

                return Enumerable.Empty<CatalogAttributeGroupDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for CategoryId: {CategoryId}, SubCategoryId: {SubCategoryId} {IsSummary}", categoryId, subCategoryId, isSummary);
                // or throw if you want to propagate the error
                return Enumerable.Empty<CatalogAttributeGroupDto>();
            }
        }

    }

}