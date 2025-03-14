namespace Logging.Domain.Repositories
{
    public interface IRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
