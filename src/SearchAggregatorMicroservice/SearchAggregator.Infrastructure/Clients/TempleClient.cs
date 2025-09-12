using SearchAggregator.Application.Clients;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Net.Http.Json;

namespace SearchAggregator.Infrastructure.Clients
{
    public class TempleClient : ITempleClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<TempleClient> _logger;

        public TempleClient(IHttpClientFactory httpClientFactory, ILoggerService<TempleClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(ITempleClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Result<SearchResultDto>>(
                    $"temple/search?q={Uri.EscapeDataString(query)}&page={page}&pageSize={pageSize}",
                    cancellationToken
                );

                if (response == null || response.Data is null)
                {
                    _logger.LogWarning("Received null response from Product service for query '{Query}'", query);
                    return new SearchResultDto(); // Return empty result
                }

                return response.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Product service for query '{Query}'", query);
                throw;
            }
        }
    }
}
