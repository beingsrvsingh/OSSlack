using SecretManagement.Domain.Entities;

namespace SecretManagement.Application.Services.Interfaces;

public interface ISecretsService
{
    Task<Dictionary<string, Secret>> GetAllSecrets(string userId);
    Task<Secret?> GetSecret(string appName, string environment, string key);
    Task CreateSecret(Secret secret);
    Task UpdateSecret(Secret secret);
    Task DeleteSecret(string key, string appName, string environment);
}
