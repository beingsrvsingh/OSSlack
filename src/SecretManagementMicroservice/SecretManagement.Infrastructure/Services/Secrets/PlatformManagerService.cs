using SecretManagement.Application.Services.Interfaces;
using Shared.Contracts.Interfaces;

namespace SecretManagement.Infrastructure.Services.Secrets;

public class PlatformManagerService : IPlatformManagerService
{
    private readonly IPlatformService _platformService;

    public PlatformManagerService(IPlatformService platformService)
    {
        this._platformService = platformService;
    }    

    public async Task<IEnumerable<string>> GetAllCredentialKeys()
    {
        return await this._platformService.GetAllCredentialKeysAsync();
    }

    public async Task<bool> AddCredential(string keyName, string secret)
    {
        return await this._platformService.AddCredentialAsync(keyName, secret);
    }

    public async Task<string?> GetCredential(string keyName)
    {
        return await this._platformService.GetCredentialAsync(keyName);
    }

    public async Task<bool> RemoveCredential(string keyName)
    {
        return await this._platformService.RemoveCredentialAsync(keyName);
    }
}