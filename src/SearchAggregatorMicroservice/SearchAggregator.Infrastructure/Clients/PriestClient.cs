using SearchAggregator.Application.Clients;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using System.Net.Http.Json;

namespace SearchAggregator.Infrastructure.Clients
{
    public class PriestClient : IPriestClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<PriestClient> _logger;

        public PriestClient(IHttpClientFactory httpClientFactory, ILoggerService<PriestClient> logger)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClient = httpClientFactory.CreateClient(nameof(IPriestClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SearchResultDto>(
                    $"api/search?query={Uri.EscapeDataString(query)}&page={page}&pageSize={pageSize}",
                    cancellationToken
                );

                if (response == null)
                {
                    _logger.LogWarning("Received null response from Product service for query '{Query}'", query);
                    return new SearchResultDto(); // Return empty result
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Product service for query '{Query}'", query);
                throw;
            }
        }
    }
}
