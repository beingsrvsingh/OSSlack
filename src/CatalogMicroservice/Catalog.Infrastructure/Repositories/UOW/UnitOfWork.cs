using Catalog.Domain.Core.Repository;
using Catalog.Domain.Core.UOW;
using Catalog.Infrastructure.Persistence.Context;
using System.Data;

namespace Catalog.Infrastructure.Repositories.UOW
{
    public class UnitOfWork(CatalogDbContext dbContext) : IUnitOfWork
    {
        protected readonly CatalogDbContext _context = dbContext;
        private bool _disposed = false;
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        private ICatalogRepository? catalogRepository;

        public ICatalogRepository CatalogRepository
        {
            get
            {
                if (catalogRepository == null)
                {
                    catalogRepository = new CatalogRepository(_context);
                }
                return catalogRepository;
            }
        }

        public IDbConnection Connection => _connection!;

        public IDbTransaction Transaction => _transaction!;

        public void Begin()
        {
            _transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction!.Commit();
            }
            catch
            {
                _transaction!.Rollback();
                throw;
            }
            finally
            {
                _transaction!.Dispose();
                _transaction = Connection.BeginTransaction();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction!.Rollback();
            }
            catch
            {
                _transaction!.Dispose();
                _transaction = Connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                Connection?.Dispose();
                _transaction?.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(true);
        }
    }
}