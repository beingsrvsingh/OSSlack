using Microsoft.EntityFrameworkCore;
using Pooja.Domain.Core.Repository;
using Pooja.Domain.Entities;
using Pooja.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Pooja.Infrastructure.Persistence.Repository
{
    public class PoojaMasterRepository : Repository<PoojaMaster>, IPoojaMasterRepository
    {
        private readonly PoojaDbContext _dbContext;

        public PoojaMasterRepository(PoojaDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PoojaMaster?> GetByIdAsync(int id) =>
        await _dbContext.PoojaMasters
            .Include(p => p.PoojaAddons)
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<PoojaMaster>> GetAllAsync() =>
            await _dbContext.PoojaMasters.ToListAsync();

        public async Task AddAsync(PoojaMaster pooja)
        {
            _dbContext.PoojaMasters.Add(pooja);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PoojaMaster pooja)
        {
            _dbContext.PoojaMasters.Update(pooja);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pooja = await _dbContext.PoojaMasters.FindAsync(id);
            if (pooja != null)
            {
                _dbContext.PoojaMasters.Remove(pooja);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PoojaMaster>> GetByTempleAsync(int templeId) =>
            await _dbContext.PoojaMasters
                //.Where(p => p.PoojaTemples.Any(pt => pt.TempleId == templeId))
                .ToListAsync();

        public async Task<IEnumerable<PoojaMaster>> GetByPriestAsync(int priestId) =>
            await _dbContext.PoojaMasters
                //.Where(p => p.PoojaPriests.Any(pp => pp.PriestId == priestId))
                .ToListAsync();
    }

}
