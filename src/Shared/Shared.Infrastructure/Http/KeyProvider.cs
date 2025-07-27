using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts;
using Shared.Contracts.Interfaces;
using Shared.Infra.Configuration;
using Shared.Utilities;
using Shared.Utilities.Response;

public class KeyProvider : IKeyValueProvider
{
    private readonly ILoggerService<KeyProvider> _logger;
    private readonly ISecretManagerClient _secretManager;
    private readonly SecretOptions _options;
    private readonly IMemoryCache _cache;
    private readonly IKeyChainConfig _keyChainConfig;
    private readonly string _prefix;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24*30);

    public KeyProvider(ILoggerService<KeyProvider> loggerService, ISecretManagerClient secretManager,
    IOptions<SecretOptions> options, IMemoryCache cache, IKeyChainConfig keyChainConfig)
    {
        this._logger = loggerService;
        _secretManager = secretManager;
        _options = options.Value;
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        this._keyChainConfig = keyChainConfig;
        _prefix = _keyChainConfig.EnvPrefix;
    }

    public string GetValue(string resourcePath, string keyName)
    {

        if (_cache.TryGetValue(keyName, out string? cachedSecret))
        {
            _logger.LogInfo("Secret for key '{KeyName}' retrieved from cache.", keyName);
            return cachedSecret;
        }

        var secretValue = _secretManager.GetSecretKeyAsync(resourcePath, keyName)
                                       .ConfigureAwait(false)
                                       .GetAwaiter()
                                       .GetResult();

        if (string.IsNullOrWhiteSpace(secretValue))
        {
            _logger.LogInfo("Secret key '{KeyName}' retrieved is null or empty.", keyName);
            throw new InvalidOperationException($"Key '{keyName}' is missing.");
        }

        return ProcessSecretValue(keyName, secretValue);
    }

    private string ProcessSecretValue(string keyName, string response)
    {        
        // Deserialize JSON into Result
        var keyResult = JsonSerializerWrapper.Deserialize<Result>(response)
            ?? throw new InvalidOperationException("Failed to deserialize signing key result.");

        // Extract the key safely
        var result = keyResult.Data;

        if (result is JsonElement element && element.ValueKind == JsonValueKind.Array)
        {
            throw new InvalidOperationException("SecretManager returned no usable key in response.");
        }

        string prefixKeyName = ($"{_prefix}:{keyName}").ToLowerInvariant();
        _cache.Set(prefixKeyName, result.ToString(), _cacheDuration);
        _logger.LogInfo("Secret for key '{KeyName}' cached for {Duration} minutes.", prefixKeyName, _cacheDuration.TotalMinutes);

        return result.ToString();
    }
}
