namespace Shared.Domain.Repository
{
    public interface IRepository<T> : IBaseRepositoryAsync<T> where T : class { }
}
