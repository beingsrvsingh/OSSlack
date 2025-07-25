using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.Interfaces;
using Shared.Utilities;

namespace Shared.Infrastructure.Platform;

public class DynamicPlatformService : IPlatformService
{
    private readonly RemoteSecretManager _remoteManager;
    private readonly WindowsSecretManager _windowsManager;
    private readonly MacKeychainManager _macManager;
    private readonly LinuxSecretManager _linuxManager;
    private readonly IConfiguration _configuration;
    private readonly IConfigurationRoot _config;
    private readonly IPlatform _platform;

    public DynamicPlatformService(
        IPlatform platform,
        RemoteSecretManager remoteManager,
        WindowsSecretManager windowsManager,
        MacKeychainManager macManager,
        LinuxSecretManager linuxManager,
        IConfiguration configuration)
    {
        _platform = platform;
        _remoteManager = remoteManager;
        _windowsManager = windowsManager;
        _macManager = macManager;
        _linuxManager = linuxManager;
        _configuration = configuration;
        _config = Helper.LoadAppSettings();        
    }

    private bool UseRemote => _config.GetSection("Secrets").GetValue<bool>("UseRemote");

    private IPlatform Current =>
        UseRemote
            ? _remoteManager
            : RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? _windowsManager
            : RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? _macManager
            : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? _linuxManager
            : throw new PlatformNotSupportedException("Unsupported OS platform");

    public Task<IEnumerable<string>> GetAllCredentialKeysAsync() => Current.GetAllCredentialKeysAsync();
    public Task<string?> GetCredentialAsync(string keyName) => Current.GetCredentialAsync(keyName);
    public Task<bool> AddCredentialAsync(string keyName, string secret) => Current.AddCredentialAsync(keyName, secret);
    public Task<bool> RemoveCredentialAsync(string keyName) => Current.RemoveCredentialAsync(keyName); 
}

