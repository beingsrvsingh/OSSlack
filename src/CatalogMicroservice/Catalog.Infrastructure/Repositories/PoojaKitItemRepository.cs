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

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PoojaKitItems.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }
            _context.PoojaKitItems.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PoojaKitItemMaster>> GetByKitItemIdAsync(int poojaKitItemId)
        {
            return await _context.PoojaKitItems
                                 .Where(x => x.Id == poojaKitItemId)
                                 .ToListAsync();
        }

    }
}