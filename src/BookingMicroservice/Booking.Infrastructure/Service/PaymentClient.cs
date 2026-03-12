using Booking.Application.Contracts;
using Booking.Application.Service;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace Booking.Infrastructure.Service
{
    public class PaymentClient : IPaymentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<PaymentClient> _logger;

        public PaymentClient(IHttpClientFactory httpClientFactory, ILoggerService<PaymentClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IPaymentClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaymentResponse?> Payment(string orderNumber, string userId, decimal amount)
        {
            try
            {
                var body = JsonContent.Create(new
                {
                    OrderNumber = orderNumber,
                    UserId = userId,
                    Amount = amount
                });

                var response = await _httpClient.PostAsync("create", body);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<PaymentResponse>>();

                return result?.Data ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Payment MS for OrderNumber: {OrderNumber}", orderNumber);
                return null;
            }
        }
    }
}
