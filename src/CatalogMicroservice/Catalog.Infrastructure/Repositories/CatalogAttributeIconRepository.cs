using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogAttributeIconRepository : Repository<CatalogAttributeIcon>, ICatalogAttributeIconRepository
    {
        private readonly CatalogDbContext dbContext;

        public CatalogAttributeIconRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}