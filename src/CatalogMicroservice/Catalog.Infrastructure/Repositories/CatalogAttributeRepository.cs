using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogAttributeRepository : Repository<CatalogAttribute>, ICatalogAttributeRepository
    {
        private readonly CatalogDbContext dbContext;

        public CatalogAttributeRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CatalogAttribute>> GetAttributesByCategoryIdAsync(int categoryId)
        {
            return await dbContext.CatalogAttributes
                .Include(attr => attr.AllowedValues)
                .Where(attr => attr.SubCategoryMasterId == categoryId)
                .OrderBy(attr => attr.SortOrder)
                .ToListAsync();
        }
    }
}