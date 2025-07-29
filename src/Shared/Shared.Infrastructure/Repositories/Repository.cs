using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Domain.Repository;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        internal DbSet<T> dbSet { get; set; }

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return dbSet.AsQueryable();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this.dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, bool disableTracking = true)
        {
            IQueryable<T> query = this.dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeProperties)) query = query.Include(includeProperties);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = this.dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T1>> GetByAsync<T1>(Expression<Func<T, bool>> predicate, Expression<Func<T, T1>> selector)
        {
            return await _dbContext.Set<T>().Where(predicate).Select(selector).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async virtual Task<T?> GetByAsync(Expression<Func<T, bool>> exp)
        {
            return await this.dbSet.AsNoTracking().FirstOrDefaultAsync(exp);
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<T, bool>> exp)
        {
            return await this.dbSet.AnyAsync(exp);
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public virtual async Task<T?> SingleOrDefaultAsync()
        {
            return await this.dbSet.AsNoTracking<T>().SingleOrDefaultAsync();
        }

        public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.AsNoTracking<T>().AsNoTracking<T>().SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<T?> FirstOrDefaultAsync()
        {
            return await this.dbSet.AsNoTracking<T>().FirstOrDefaultAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.AsNoTracking<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy(string orderColumn, string orderType)
        {
            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumn.Split('.');
            IQueryable<T> query = new List<T>().AsQueryable<T>();
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");

            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!;
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }

        public async Task<(List<T> Items, int TotalCount)> GetPaginatedAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            int totalCount = await query.CountAsync(cancellationToken);

            if (orderBy != null)
                query = orderBy(query);
            else
                query = query.OrderBy(e => true); // fallback to default ordering

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task BulkInsertAsync(
        IList<T> entities,
        int? batchSize = null,
        CancellationToken cancellationToken = default)
        {
            if (entities == null || entities.Count == 0)
                return;

            await this._dbContext.BulkInsertAsync(entities, new BulkConfig
            {
                BatchSize = batchSize ?? 10000,
                PreserveInsertOrder = true,
                SetOutputIdentity = true
            }, progress: null, type: null, cancellationToken);
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public async virtual Task AddRangeAsync(params T[] entities)
        {
            await this.dbSet.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public virtual Task UpdateAsync(T entity)
        {
            this.dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T entity)
        {
            dbSet.Attach(entity);
            dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = this.dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                this.dbSet.Remove(obj);
            }
            return Task.CompletedTask;
        }

        public async virtual Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}
