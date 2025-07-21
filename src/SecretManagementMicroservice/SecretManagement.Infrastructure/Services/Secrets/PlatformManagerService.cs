using SecretManagement.Application.Services.Interfaces;
using Shared.Contracts.Interfaces;
using Shared.Infrastructure.Platform;

namespace SecretManagement.Infrastructure.Services.Secrets;

public class PlatformManagerService : IPlatformManagerService
{
    private readonly IPlatformService _platformService;

    public PlatformManagerService()
    {
        this._platformService = PlatformServiceFactory.Create();
    }    

    public IEnumerable<string> GetAllCredentialKeys()
    {
        return this._platformService.GetAllCredentialKeys();
    }

    public void AddCredential(string keyName, string secret)
    {
        this._platformService.AddCredential(keyName, secret);
    }

    public string? GetCredential(string keyName)
    {
        return this._platformService.GetCredential(keyName);
    }

    public void RemoveCredential(string keyName)
    {
        this._platformService.RemoveCredential(keyName);
    }
}