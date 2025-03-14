using System.Linq.Expressions;

namespace Shared.Domain.Repository
{
    public interface IBaseRepositoryAsync<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        string? includeProperties = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
        IEnumerable<T1> GetBy<T1>(Expression<Func<T, bool>> exp, Expression<Func<T, T1>> columns);

        Task<T?> GetBy(Expression<Func<T, bool>> exp);

        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(string id);
        Task<T?> SingleOrDefaultAsync();
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy(string orderColumn, string orderType);
        T AddAsync(T entity);
        Task AddRangeAsync(params T[] entities);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        void Delete(Expression<Func<T, bool>> where);

        void Save();
    }
}
