
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts.Interfaces;
using Shared.Infra.Configuration;

public class SigningKeyProvider : ISigningKeyProvider
{
    private readonly ILoggerService<SigningKeyProvider> _logger;
    private readonly ISecretManagerClient _secretManager;
    private readonly SecretOptions _options;

    public SigningKeyProvider(ILoggerService<SigningKeyProvider> loggerService, ISecretManagerClient secretManager, IOptions<SecretOptions> options)
    {
        this._logger = loggerService;
        _secretManager = secretManager;
        _options = options.Value;
    }

    public string GetSigningKey()
    {        
        var signingKey = _secretManager.GetSecretKeyAsync("jwt-signing-key")
                                       //.ConfigureAwait(false)
                                       .GetAwaiter()
                                       .GetResult();
        
        if (string.IsNullOrWhiteSpace(signingKey))
        {
            _logger.LogInfo("Signing key retrieved is null or empty.");
            throw new InvalidOperationException("Signing key is missing.");
        }

        return signingKey;
    }
}
