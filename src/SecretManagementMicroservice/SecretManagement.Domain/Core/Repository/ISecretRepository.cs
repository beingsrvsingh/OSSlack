using SecretManagement.Domain.Entities;

namespace SecretManagement.Domain.Core.Repository;
public interface ISecretRepository : IRepository<Secret>
{
    Task<Dictionary<string, Secret>> GetAllAsync(string userId);
}
