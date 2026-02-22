using Astrologer.Application.Service;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace Astrologer.Infrastructure.Service
{
    public class BookingClient : IBookingClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<BookingClient> _logger;

        public BookingClient(IHttpClientFactory httpClientFactory, ILoggerService<BookingClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IBookingClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<BookingResponseDto>> GetBookingsByDateAsync(int entityId, DateTime date)
        {
            try
            {
                var response = new List<BookingResponseDto>(); //await _httpClient.GetFromJsonAsync<BookingResponseDto>>($"astrologer/search?q={Uri.EscapeDataString(query)}&page={page}&pageSize={pageSize}",cancellationToken);

                return response;

                //if (response == null || response.Data is null)
                //{
                //    _logger.LogWarning("Received null response from Booking service for query '{Query}'", entityId);
                //    return new List<BookingResponseDto>();
                //}

                //return response.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Booking service for query '{Query}'", entityId);
                throw;
            }
        }
    }
}
