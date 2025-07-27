using System.Text;
using Microsoft.AspNetCore.Hosting;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts;
using Shared.Contracts.Interfaces;
using Shared.Utilities;

namespace Shared.Infrastructure.Platform;

public class LinuxSecretManager : IPlatform
{
    private readonly string _basePath;
    private readonly string _envPrefix;
    private readonly ILoggerService<LinuxSecretManager> _logger;
    private readonly IKeyChainConfig _keyChainConfig;

    public LinuxSecretManager(ILoggerService<LinuxSecretManager> logger, IKeyChainConfig keyChainConfig)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._keyChainConfig = keyChainConfig;
        _envPrefix = _keyChainConfig.EnvPrefix;
        _basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".linuxsecrets");
        Directory.CreateDirectory(_basePath);
    }

    public Task<IEnumerable<string>> GetAllCredentialKeysAsync()
    {
        try
        {
            if (!Directory.Exists(_basePath))
                return Task.FromResult(Enumerable.Empty<string>());

            var files = Directory.GetFiles(_basePath, "*.secret");

            var keys = files
                .Select(Path.GetFileNameWithoutExtension)
                .Where(name => name.StartsWith(_envPrefix, StringComparison.OrdinalIgnoreCase))
                .Select(name => name.Substring(_envPrefix.Length))
                .ToList();

            return Task.FromResult<IEnumerable<string>>(keys);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all credential keys.");
            return Task.FromResult(Enumerable.Empty<string>());
        }
    }

    public async Task<bool> AddCredentialAsync(string keyName, string secret)
    {
        var envKeyName = _keyChainConfig.AddEnvPrefix(keyName);
        var filePath = GetSecretPath(envKeyName);

        try
        {
            await File.WriteAllTextAsync(filePath, secret);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding credential for key '{envKeyName}'.");
            return false;
        }
    }

    public async Task<string?> GetCredentialAsync(string keyName)
    {
        var envKeyName = _keyChainConfig.AddEnvPrefix(keyName);
        var filePath = GetSecretPath(envKeyName);

        try
        {
            if (!File.Exists(filePath))
                return null;

            return await File.ReadAllTextAsync(filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error reading credential for key '{envKeyName}'.");
            return null;
        }
    }

    public Task<bool> RemoveCredentialAsync(string keyName)
    {
        var envKeyName = _keyChainConfig.AddEnvPrefix(keyName);
        var filePath = GetSecretPath(envKeyName);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error removing credential for key '{envKeyName}'.");
            return Task.FromResult(false);
        }
    }

    private string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = new StringBuilder(fileName.Length);
        foreach (var c in fileName)
        {
            sanitized.Append(invalidChars.Contains(c) ? '_' : c);
        }
        return sanitized.ToString();
    }

    private string GetSecretPath(string envKeyName)
    {
        var sanitized = SanitizeFileName(envKeyName);
        return Path.Combine(_basePath, $"{sanitized}.secret");
    }
}