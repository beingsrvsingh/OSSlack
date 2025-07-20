using Catalog.Domain.Core.Repository;
using Catalog.Domain.Core.UOW;
using Catalog.Infrastructure.Persistence.Context;
using Shared.Infrastructur.UoW;

namespace Catalog.Infrastructure.Repositories.UOW
{
    public class UnitOfWork : BaseUnitOfWork<CatalogDbContext>, IUnitOfWork
    {
        public UnitOfWork(
            CatalogDbContext context
        ) : base(context)
        {
        }

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
    }
}