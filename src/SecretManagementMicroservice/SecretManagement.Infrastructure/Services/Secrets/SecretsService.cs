using System.Threading.Tasks;
using SecretManagement.Application.Services.Interfaces;
using SecretManagement.Domain.Core.UOW;
using SecretManagement.Domain.Entities;

namespace SecretManagement.Infrastructure.Services.Secrets;

public class SecretsService : ISecretsService
{
    private readonly IUnitOfWork _uow;

    public SecretsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Dictionary<string, Secret>> GetAllSecrets(string userId)
    {
        return await _uow.SecretRepository.GetAllAsync(userId);
    }

    public async Task<Secret?> GetSecret(string appName, string environment, string key)
    {
        return await _uow.SecretRepository.GetByAsync(s =>
             s.AppName == appName &&
             s.Environment == environment &&
             s.SecretKey == key &&
             s.IsActive);
    }

    public async Task CreateSecret(Secret secret)
    {
        secret.CreatedAt = DateTime.UtcNow;
        secret.IsActive = true;
        secret.Version = 1;

        bool exists = await HasSecret(secret.AppName, secret.Environment, secret.SecretKey);

        if (exists)
            throw new InvalidOperationException("Secret already exists and is active.");

        secret.CreatedAt = DateTime.UtcNow;
        secret.CreatedBy = "System";
        secret.Version = 1;
        secret.IsActive = true;

        await this._uow.SecretRepository.AddAsync(secret);
        await this._uow.SecretRepository.SaveChangesAsync();
    }

    public async Task UpdateSecret(Secret secret)
    {
        Secret? existingSecret = null;
        try
        {
            existingSecret = await this._uow.SecretRepository.GetByAsync(s =>
                s.IsActive);

            if (existingSecret == null)
                throw new KeyNotFoundException("Secret not found or inactive.");
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving existing secret", ex);
        }

        if (existingSecret == null)
            throw new KeyNotFoundException("Secret not found or inactive.");

        existingSecret.SecretValue = secret.SecretValue;
        existingSecret.CreatedAt = secret.CreatedAt;
        existingSecret.CreatedBy = "System";
        existingSecret.Version = existingSecret.Version + 1;
        secret.IsActive = true;
        secret.UpdatedAt = DateTime.UtcNow;

        try
        {
            await this._uow.SecretRepository.UpdateAsync(existingSecret);
            await this._uow.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating secret", ex);
        }
    }

    public async Task DeleteSecret(string key, string appName, string environment)
    {
        Secret? existingSecret = await this._uow.SecretRepository.GetByAsync(s =>
            s.AppName == appName &&
            s.Environment == environment &&
            s.SecretKey == key &&
            s.IsActive);

        if (existingSecret == null)
            throw new KeyNotFoundException("Secret not found or inactive.");

        existingSecret.IsActive = false;
        existingSecret.UpdatedAt = DateTime.UtcNow;

        await _uow.SecretRepository.DeleteAsync(existingSecret);
    }    
    
    private async Task<bool> HasSecret(string appName, string environment, string key)
    {
        try
        {
            var secret = await _uow.SecretRepository.GetByAsync(s =>
                s.AppName == appName &&
                s.Environment == environment &&
                s.SecretKey == key &&
                s.IsActive);

            return secret != null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error checking if secret exists", ex);
        }
    }
}
