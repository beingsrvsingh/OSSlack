using SecretManagement.Domain.Entities;
using System.Threading.Tasks;

namespace SecretManagement.Application.Services.Interfaces;

public interface ISecretsService
{
    Task<bool> HasSecret(string appName, string environment, string key);
    Task<Secret?> GetSecret(string appName, string environment, string key);
    Task CreateSecret(Secret secret);
    Task UpdateSecret(Secret secret);
    Task DeleteSecret(string appName, string environment, string key);
    Dictionary<string, Secret> GetAllSecrets(string appName, string environment);
}
