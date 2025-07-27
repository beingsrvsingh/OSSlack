using Microsoft.Extensions.Caching.Memory;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;

namespace Shared.Infrastructure.Http;

public class SecretManagerClient : ISecretManagerClient
{
    private readonly HttpClient _httpClient;
    private readonly ILoggerService<SecretManagerClient> _logger;

    public SecretManagerClient(
        ILoggerService<SecretManagerClient> logger,
        IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(SecretManagerClient)) ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));        
    }

    public async Task<string?> GetSecretKeyAsync(string resourcePath, string keyName, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{resourcePath}/{keyName}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to retrieve secret for key '{KeyName}'. StatusCode: {StatusCode}", keyName, response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);            

            return content;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for key '{KeyName}'", keyName);
            return null;
        }
        catch (TaskCanceledException)
        {
            _logger.LogWarning("Request canceled for key '{KeyName}'", keyName);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error retrieving secret for key '{KeyName}'", keyName);
            return null;
        }
    }
}
