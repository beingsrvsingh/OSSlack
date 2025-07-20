using SecretManagement.Domain.Entities;

namespace SecretManagement.Domain.Core.Repository;
public interface ISecretRepository : IRepository<Secret>
{
    Dictionary<string, Secret> GetAll(string appName, string environment);
}
