
namespace SecretManagement.Application.Services.Interfaces;

public interface IPlatformManagerService
{
    Task<IEnumerable<string>> GetAllCredentialKeys();
    Task<string?> GetCredential(string keyName);

    Task<bool> AddCredential(string keyName, string secret);
    Task<bool> RemoveCredential(string keyName);
}