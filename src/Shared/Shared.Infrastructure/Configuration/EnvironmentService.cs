using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts;
using Shared.Contracts.Interfaces;
using Shared.Utilities;
using Shared.Utilities.Response;

namespace Shared.Infrastructure.Configuration
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly ILoggerService<EnvironmentService> _logger;
        private readonly IKeyChainConfig _keyChainConfig;
        private readonly string _prefix;

        public EnvironmentService(ILoggerService<EnvironmentService> logger, IKeyChainConfig keyChainConfig)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._keyChainConfig = keyChainConfig ?? throw new ArgumentNullException(nameof(keyChainConfig));
            _prefix = _keyChainConfig.EnvPrefix;
        }

        public string? GetVariable(string key)
        {
            string prefixKeyName = _keyChainConfig.AddEnvPrefix(key).ToUpperInvariant();
            try
            {
                var value = Environment.GetEnvironmentVariable(prefixKeyName);
                _logger.LogInfo($"GetVariable: Key='{key}' Value='{(value != null ? "***" : "null")}'");
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get environment variable {prefixKeyName}");
                return null;
            }
        }

        public T? GetJsonObject<T>(string key) where T : class
        {
            var json = GetVariable(key);
            if (string.IsNullOrWhiteSpace(json))
            {
                _logger.LogInfo($"GetJsonObject: Key='{key}' is empty or null");
                return null;
            }

            try
            {
                var obj = JsonSerializer.Deserialize<T>(json);
                _logger.LogInfo($"GetJsonObject: Successfully deserialized key='{key}'");
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to deserialize JSON for environment variable '{key}'");
                return null;
            }
        }

        public bool IsSet(string key)
        {
            var isSet = !string.IsNullOrEmpty(GetVariable(key));
            _logger.LogInfo($"IsSet: Key='{key}' IsSet={isSet}");
            return isSet;
        }

        public Task<bool> SetVariable(string key, string value)
        {
            string prefixKeyName = _keyChainConfig.AddEnvPrefix(key).ToUpperInvariant();
            try
            {
                Environment.SetEnvironmentVariable($"{prefixKeyName}", value);
                _logger.LogInfo($"SetVariable: Key='{key}' set successfully");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to set environment variable {prefixKeyName}");
                return Task.FromResult(false);
            }
        }

        public Task<bool> RemoveVariable(string key)
        {
            string prefixKeyName = _keyChainConfig.AddEnvPrefix(key).ToUpperInvariant();
            try
            {
                Environment.SetEnvironmentVariable($"{prefixKeyName}", null);
                _logger.LogInfo($"RemoveVariable: Key='{key}' removed successfully");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to remove environment variable {prefixKeyName}");
                return Task.FromResult(false);
            }
        }
    }
}
