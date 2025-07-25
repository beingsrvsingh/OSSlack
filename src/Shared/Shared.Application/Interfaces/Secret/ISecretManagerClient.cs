
namespace Shared.Application.Interfaces;
public interface ISecretManagerClient
{
    Task<string?> GetSecretKeyAsync(string keyName, CancellationToken cancellationToken = default);
}