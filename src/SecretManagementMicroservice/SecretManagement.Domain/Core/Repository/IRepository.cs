using Shared.Domain.Repository;

namespace SecretManagement.Domain.Core.Repository
{
    public interface IRepository<T> : IBaseRepositoryAsync<T> where T : class { }
}
