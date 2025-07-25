using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts.Interfaces;

namespace Shared.Infrastructure.Http;

public class SecretManagerClient : ISecretManagerClient
{
    private readonly HttpClient _httpClient;
    private readonly ILoggerService<SecretManagerClient> _logger;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24*30);

    public SecretManagerClient(
        ILoggerService<SecretManagerClient> logger,
        IHttpClientFactory httpClientFactory,        
        IMemoryCache cache)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(SecretManagerClient)) ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<string?> GetSecretKeyAsync(string keyName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(keyName))
        {
            _logger.LogWarning("Key name is null or whitespace.");
            return null;
        }

        if (_cache.TryGetValue(keyName, out string? cachedSecret))
        {
            _logger.LogInfo("Secret for key '{KeyName}' retrieved from cache.", keyName);
            return cachedSecret;
        }

        try
        {
            var response = await _httpClient.GetAsync($"platform/{keyName}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to retrieve secret for key '{KeyName}'. StatusCode: {StatusCode}", keyName, response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
                return null;

            _cache.Set(keyName, content, _cacheDuration);
            _logger.LogInfo("Secret for key '{KeyName}' cached for {Duration} minutes.", keyName, _cacheDuration.TotalMinutes);

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
