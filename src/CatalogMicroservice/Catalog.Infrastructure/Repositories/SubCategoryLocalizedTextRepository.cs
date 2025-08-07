using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class SubCategoryLocalizedTextRepository : Repository<SubCategoryLocalizedText>, ISubCategoryLocalizedTextRepository
    {
        private readonly CatalogDbContext _context;

        public SubCategoryLocalizedTextRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<SubCategoryLocalizedText>> GetBySubCategoryIdAsync(int subCategoryId)
        {
            return await _context.SubCategoryLocalizedTexts
                .Where(x => x.SubCategoryId == subCategoryId && x.LocalizedName == "SubCategory")
                .ToListAsync();
        }
    }
}