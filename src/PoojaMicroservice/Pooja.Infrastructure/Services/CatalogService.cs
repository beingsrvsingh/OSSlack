using Pooja.Application.Contracts;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace Pooja.Infrastructure.Services
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

        public async Task<IEnumerable<CatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false)
        {
            try
            {
                var url = $"category/attributes?CategoryId={categoryId}&SubCategoryId={subCategoryId}&IsSummary={isSummary}";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<CatalogAttributeDto>>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? Enumerable.Empty<CatalogAttributeDto>();
                }

                return Enumerable.Empty<CatalogAttributeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for CategoryId: {CategoryId}, SubCategoryId: {SubCategoryId} {IsSummary}", categoryId, subCategoryId, isSummary);
                // or throw if you want to propagate the error
                return Enumerable.Empty<CatalogAttributeDto>();
            }
        }

        public async Task<IEnumerable<CatalogAttributeGroupDto>> GetGroupedAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false)
        {
            try
            {
                var url = $"category/group-attributes?CategoryId={categoryId}&SubCategoryId={subCategoryId}&IsSummary={isSummary}";

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

        public async Task<IEnumerable<FilterableAttributeDto>> GetFilterableAttributeById(int categoryId, int subCategoryId, bool isSummary = false)
        {
            try
            {
                var url = $"category/filterable-attributes?CategoryId={categoryId}&SubCategoryId={subCategoryId}&IsSummary={isSummary}";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<FilterableAttributeDto>>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? Enumerable.Empty<FilterableAttributeDto>();
                }

                return Enumerable.Empty<FilterableAttributeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for CategoryId: {CategoryId}, SubCategoryId: {SubCategoryId} {IsSummary}", categoryId, subCategoryId, isSummary);
                // or throw if you want to propagate the error
                return Enumerable.Empty<FilterableAttributeDto>();
            }
        }
    }
}
