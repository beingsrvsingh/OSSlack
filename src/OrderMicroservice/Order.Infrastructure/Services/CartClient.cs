using Order.Application.Contracts;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace Order.Infrastructure.Services
{
    public class CartClient : ICartClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<CartClient> _logger;

        public CartClient(IHttpClientFactory httpClientFactory, ILoggerService<CartClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(ICartClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CartDto?> GetCartInfoByUserIdAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"cart/{userId}");

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<CartDto>>();

                return result?.Data ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for OrderId: {OrderId}", userId);
                return null;
            }
        }
    }
}
