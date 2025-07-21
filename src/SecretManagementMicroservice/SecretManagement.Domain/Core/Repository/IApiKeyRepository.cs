using SecretManagement.Domain.Entities;

namespace SecretManagement.Domain.Core.Repository;
public interface IApiKeyRepository : IRepository<ApiSecret>
{
}
