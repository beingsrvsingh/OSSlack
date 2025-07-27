
namespace Shared.Application.Interfaces;
public interface ISecretManagerClient
{
    Task<string?> GetSecretKeyAsync(string resourcePath, string keyName, CancellationToken cancellationToken = default);
}