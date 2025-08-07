using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class PoojaKitItemLocalizedTextRepository : Repository<PoojaKitItemLocalizedText>, IPoojaKitItemLocalizedTextRepository
    {
        private readonly CatalogDbContext _context;

        public PoojaKitItemLocalizedTextRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<PoojaKitItemLocalizedText>> GetByItemIdAsync(int itemId)
        {
            return await _context.PoojaKitItemTags
                                 .Where(x => x.PoojaKitId == itemId)
                                 .ToListAsync();
        }
    }
}