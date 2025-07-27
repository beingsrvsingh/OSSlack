using Microsoft.AspNetCore.Hosting;
using Shared.Contracts;

namespace Shared.Infrastructure.Platform;

public class KeyChainConfig : IKeyChainConfig
{
    private readonly IWebHostEnvironment _env;
    private const string DefaultKeyChainName = "app.keychain-db";
    private const string DefaultKeyChainPassword = "password";

    public KeyChainConfig(IWebHostEnvironment environment)
    {
        if (environment == null) throw new ArgumentNullException(nameof(environment));
        this._env = environment;
    }

    public string EnvPrefix
    {
        get
        {
            string envName = _env.EnvironmentName;
            if (string.IsNullOrEmpty(envName))
                return "DEV";

            return envName.ToLowerInvariant() switch
            {
                "development" => "DEV",
                "testing" => "TEST",
                "staging" => "STAGING",
                "production" => "PROD",
                _ => "DEV"
            };
        }
    }

    public string KeyChainName =>
        Environment.GetEnvironmentVariable($"{EnvPrefix}_KEYCHAIN_NAME") ?? DefaultKeyChainName;

    public string KeyChainPassword =>
        Environment.GetEnvironmentVariable($"{EnvPrefix}_KEYCHAIN_PASSWORD") ?? DefaultKeyChainPassword;

    public string AddEnvPrefix(string keyName, string seperator)
    {
        string prefixKey = $"{EnvPrefix}{seperator}{keyName}";
        return prefixKey;
    }
}
