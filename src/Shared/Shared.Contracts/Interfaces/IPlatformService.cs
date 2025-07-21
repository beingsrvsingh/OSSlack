
namespace Shared.Contracts.Interfaces
{
    /// <summary>
    /// Interface for platform-specific services.
    /// </summary>
    public interface IPlatformService
    {
        IEnumerable<string> GetAllCredentialKeys();
        string? GetCredential(string keyName);

        void AddCredential(string keyName, string secret);
        void RemoveCredential(string keyName);
    }
}