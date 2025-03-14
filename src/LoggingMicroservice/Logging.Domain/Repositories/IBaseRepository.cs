using System.Linq.Expressions;

namespace Logging.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAllNonTracking();
        Task<List<T>> GetBy(Expression<Func<T, bool>> predicate);
        List<T> GetNonTrackingBy(Expression<Func<T, bool>> predicate);
        Task<T?> GetSingleBy(Expression<Func<T, bool>> predicate);
        T? GetSingleNonTrackingBy(Expression<Func<T, bool>> predicate);
        Task<int> GetCount(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void Save();
    }
}
