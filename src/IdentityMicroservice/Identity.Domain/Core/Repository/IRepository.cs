using Shared.Domain.Repository;

namespace Identity.Domain.Core.Repository
{
    public interface IRepository<T> : IBaseRepositoryAsync<T> where T : class { }
}
