using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class PoojaKitMasterRepository : Repository<PoojaKitMaster>, IPoojaKitMasterRepository
    {
        private readonly CatalogDbContext _context;

        public PoojaKitMasterRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<PoojaKitMaster>> GetBySubCategoryIdAsync(int subCategoryId)
        {
            return await _context.PoojaKits
                                 .Where(x => x.SubCategoryMasterId == subCategoryId)
                                 .ToListAsync();
        }

    }
}