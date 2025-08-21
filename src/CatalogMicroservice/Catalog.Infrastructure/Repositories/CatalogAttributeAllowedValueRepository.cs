using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogAttributeAllowedValueRepository : Repository<CatalogAttributeAllowedValue>, ICatalogAttributeAllowedValueRepository
    {
        private readonly CatalogDbContext dbContext;

        public CatalogAttributeAllowedValueRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}