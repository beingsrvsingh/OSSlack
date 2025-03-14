﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this.dbSet.AsNoTracking().ToListAsync();
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

        public IEnumerable<T1> GetBy<T1>(Expression<Func<T, bool>> exp, Expression<Func<T, T1>> columns)
        {
            return this.dbSet.Where<T>(exp).Select<T, T1>(columns);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
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

        public virtual T AddAsync(T entity)
        {
            this.dbSet.Add(entity);
            return entity;
        }

        public async virtual Task AddRangeAsync(params T[] entities)
        {
            await this.dbSet.AddRangeAsync(entities);
        }

        public virtual void UpdateAsync(T entity)
        {
            this.dbSet.Update(entity);
        }

        public virtual void DeleteAsync(T entity)
        {
            this.dbSet.Attach(entity);
            this.dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = this.dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                this.dbSet.Remove(obj);
            }
        }

        public virtual void Save()
        {
            this._dbContext.SaveChanges();
        }

        public async virtual Task<T?> GetBy(Expression<Func<T, bool>> exp)
        {
           return await this.dbSet.AsQueryable().FirstOrDefaultAsync(exp);
        }        
    }
}
