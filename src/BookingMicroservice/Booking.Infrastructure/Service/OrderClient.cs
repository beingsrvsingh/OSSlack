using Booking.Application.Contracts;
using Booking.Application.Service;
using BookingMicroservice.Domain.Entities;
using MongoDB.Driver.Core.Operations;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace Booking.Infrastructure.Service
{
    public class OrderClient : IOrderClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<OrderClient> _logger;

        public OrderClient(IHttpClientFactory httpClientFactory, ILoggerService<OrderClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IOrderClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OrderResponse?> AddOrderAsync(string bookingRefNum)
        {
            try
            {
                var body = JsonContent.Create(new { bookingRefNum = bookingRefNum });

                var response = await _httpClient.PostAsync($"order", body);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<OrderResponse>>();

                return result?.Data ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Order MS for BookingRefNum: {bookingRefNum}", bookingRefNum);
                return null;
            }
        }

        public async Task<bool> UpdateStatusOrderAsync(string orderNumber, string status)
        {
            int retryLimit = 5;

            for (int retry = 0; retry < retryLimit; retry++)
            {
                try
                {
                    var body = JsonContent.Create(new { orderNumber, status });

                    var response = await _httpClient.PatchAsync("order/status", body);

                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadFromJsonAsync<Result<bool>>();

                    return result?.Data ?? false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling Order MS for OrderNumber: {orderNumber}. Retry {retry}", orderNumber, retry + 1);

                    if (retry == retryLimit - 1)
                        throw;

                    await Task.Delay(1000);
                }
            }

            return false;
        }
    }
}
