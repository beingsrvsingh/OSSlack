using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class PoojaKitItemRepository : Repository<PoojaKitItemMaster>, IPoojaKitItemRepository
    {
        private readonly CatalogDbContext _context;

        public PoojaKitItemRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<PoojaKitItemLocalizedText> AddLocalizationAsync(PoojaKitItemLocalizedText localization)
        {
            _context.PoojaKitItemTags.Add(localization);
            await _context.SaveChangesAsync();
            return localization;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PoojaKitItemTags.FindAsync(id);
            if (entity == null)
                return false;

            _context.PoojaKitItemTags.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<PoojaKitItemLocalizedText>> GetLocalizationsAsync(int itemId)
        {
            return await _context.PoojaKitItemTags
                                 .Where(x => x.PoojaKitId == itemId)
                                 .ToListAsync();
        }

    }
}