using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class PoojaKitItemMasterRepository : Repository<PoojaKitItemMaster>, IPoojaKitItemMasterRepository
    {
        private readonly CatalogDbContext _context;

        public PoojaKitItemMasterRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<PoojaKitItemMaster>> GetByKitIdAsync(int poojaKitId)
        {
            return await _context.PoojaKitItems
                                 .Where(x => x.PoojaKitMasterId == poojaKitId)
                                 .ToListAsync();
        }

    }
}