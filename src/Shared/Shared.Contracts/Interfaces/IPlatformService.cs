
namespace Shared.Contracts.Interfaces
{
    /// <summary>
    /// Interface for platform-specific services.
    /// </summary>
    public interface IPlatformService : IPlatform
    {        
    }

    public interface IPlatform
    {
        Task<IEnumerable<string>> GetAllCredentialKeysAsync();
        Task<string?> GetCredentialAsync(string keyName);

        Task<bool> AddCredentialAsync(string keyName, string secret);
        Task<bool> RemoveCredentialAsync(string keyName);
    }
}