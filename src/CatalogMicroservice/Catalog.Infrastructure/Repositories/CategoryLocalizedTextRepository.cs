using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryLocalizedTextRepository : Repository<CategoryLocalizedText>, ICategoryLocalizedTextRepository
    {
        private readonly CatalogDbContext dbContext;

        public CategoryLocalizedTextRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryLocalizedText>> GetByCategoryIdAsync(int categoryId)
        {
            return await dbContext.CategoryLocalizedTexts
                         .Where(clt => clt.CategoryId == categoryId)
                         .ToListAsync();
        }
    }
}