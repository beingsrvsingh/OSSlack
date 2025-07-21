
namespace SecretManagement.Application.Services.Interfaces;

public interface IPlatformManagerService
{
    IEnumerable<string> GetAllCredentialKeys();
    string? GetCredential(string keyName);

    void AddCredential(string keyName, string secret);
    void RemoveCredential(string keyName);
}