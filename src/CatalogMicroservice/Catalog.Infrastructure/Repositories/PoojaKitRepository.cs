using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class PoojaKitRepository : Repository<PoojaKitMaster>, IPoojaKitRepository
    {
        private readonly CatalogDbContext _context;

        public PoojaKitRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<PoojaKitLocalizedText> AddLocalizationAsync(PoojaKitLocalizedText localization)
        {
            _context.PoojaKitLocalizedTexts.Add(localization);
            await _context.SaveChangesAsync();
            return localization;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PoojaKitLocalizedTexts.FindAsync(id);
            if (entity == null)
                return false;

            _context.PoojaKitLocalizedTexts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PoojaKitLocalizedText>> GetLocalizationsAsync(int kitId)
        {
            return await _context.PoojaKitLocalizedTexts
                                 .Where(x => x.PoojaKitId == kitId && x.LocalizedName == "PoojaKit")
                                 .ToListAsync();
        }
    }
}