
using System.Net.Http.Json;
using Order.Application.Services;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<PaymentService> _logger;

        public PaymentService(IHttpClientFactory httpClientFactory, ILoggerService<PaymentService> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IPaymentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaymentInfoDto?> GetPaymentInfoByIdAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"summary/{orderId}");

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<PaymentInfoDto>>();

                return result?.Data ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Catalog MS for OrderId: {OrderId}", orderId);
                return null; // or throw if required
            }
        }
    }
}