using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class PoojaKitLocalizedTextRepository : Repository<PoojaKitLocalizedText>, IPoojaKitLocalizedTextRepository
    {
        private readonly CatalogDbContext _context;

        public PoojaKitLocalizedTextRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<PoojaKitLocalizedText>> GetByKitIdAsync(int poojaKitId)
        {
            return await _context.PoojaKitLocalizedTexts
                                 .Where(x => x.PoojaKitId == poojaKitId)
                                 .ToListAsync();
        }

    }
}