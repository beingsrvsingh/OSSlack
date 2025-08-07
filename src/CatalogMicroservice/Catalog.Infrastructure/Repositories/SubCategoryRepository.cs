using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class SubCategoryRepository : Repository<SubCategoryMaster>, ISubCategoryRepository
    {
        private readonly CatalogDbContext _context;

        public SubCategoryRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<SubCategoryLocalizedText> AddLocalizationAsync(SubCategoryLocalizedText localization)
        {
            _context.SubCategoryLocalizedTexts.Add(localization);
            await _context.SaveChangesAsync();
            return localization;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubCategoryLocalizedTexts.FindAsync(id);
            if (entity == null)
                return false;

            _context.SubCategoryLocalizedTexts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SubCategoryLocalizedText>> GetLocalizationsAsync(int subCategoryId)
        {
            return await _context.SubCategoryLocalizedTexts
                .Where(x => x.SubCategoryId == subCategoryId && x.LocalizedName == "SubCategory")
                .ToListAsync();
        }
    }
}