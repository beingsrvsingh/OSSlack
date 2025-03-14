using Logging.Domain.Repositories;
using Logging.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Logging.Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LoggerContext _context;
        internal DbSet<T> dbSet { get; set; }

        public Repository(LoggerContext loggerContext)
        {
            this._context = loggerContext;
            this.dbSet = _context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            List<T> collection = dbSet.ToList();
            return collection;
        }

        public async Task<List<T>> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            List<T> collection = await dbSet.Where(predicate).AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<T?> GetSingleBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            T? entity = await dbSet.Where(predicate).FirstOrDefaultAsync();
            return entity;
        }

        public T? GetSingleNonTrackingBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            T? entity = dbSet.Where(predicate).AsNoTracking().FirstOrDefault();
            return entity;
        }

        public virtual List<T> GetAllNonTracking()
        {

            List<T> collection = dbSet.AsNoTracking().ToList();
            return collection;
        }

        public List<T> GetNonTrackingBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            List<T> collection = dbSet.Where(predicate).AsNoTracking().ToList();
            return collection;
        }

        public async Task<int> GetCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await dbSet.CountAsync(predicate);
        }


        public virtual T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual void Save()
        {
           _context.SaveChanges();
        }
    }
}