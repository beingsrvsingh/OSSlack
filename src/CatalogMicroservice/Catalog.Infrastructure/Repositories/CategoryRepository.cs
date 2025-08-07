using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<CategoryMaster>, ICategoryRepository
    {
        private readonly CatalogDbContext _context;

        public CategoryRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<CategoryLocalizedText> AddLocalizationAsync(CategoryLocalizedText localization)
        {
            await _context.CategoryLocalizedTexts.AddAsync(localization);
            await _context.SaveChangesAsync();
            return localization;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CategoryLocalizedTexts.FindAsync(id);
            if (entity == null)
                return false;

            _context.CategoryLocalizedTexts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryLocalizedText>> GetLocalizationsAsync(int categoryId)
        {
            return await _context.CategoryLocalizedTexts
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
