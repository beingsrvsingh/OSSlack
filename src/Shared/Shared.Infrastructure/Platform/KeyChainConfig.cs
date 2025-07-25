using Microsoft.AspNetCore.Hosting;
using Shared.Contracts;
using Shared.Utilities;

namespace Shared.Infrastructure.Platform;

public class KeyChainConfig : IKeyChainConfig
{
    private readonly string _prefix;

    private const string DefaultKeyChainName = "app.keychain-db";
    private const string DefaultKeyChainPassword = "password";

    public KeyChainConfig(IWebHostEnvironment environment)
    {
        if (environment == null) throw new ArgumentNullException(nameof(environment));

        _prefix = EnvironmentUtils.GetEnv(environment.EnvironmentName);
    }

    public string KeyChainName =>
        Environment.GetEnvironmentVariable($"{_prefix}_KEYCHAIN_NAME") ?? DefaultKeyChainName;

    public string KeyChainPassword =>
        Environment.GetEnvironmentVariable($"{_prefix}_KEYCHAIN_PASSWORD") ?? DefaultKeyChainPassword;
}
