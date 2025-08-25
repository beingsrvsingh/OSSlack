
using System.Net.Http.Json;
using Order.Application.Contracts;
using Order.Application.Services;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<AddressService> _logger;

        public AddressService(IHttpClientFactory httpClientFactory, ILoggerService<AddressService> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IAddressService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ShippingInfoDto?> GetAddressInfoByIdAsync(int addressId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"shipping/{addressId}");

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<ShippingInfoDto>>();

                return result?.Data ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for OrderId: {OrderId}", addressId);
                return null; // or throw if required
            }
        }
    }
}