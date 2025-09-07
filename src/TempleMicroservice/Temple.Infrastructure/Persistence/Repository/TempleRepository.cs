using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;
using Temple.Domain.Repositories;
using Temple.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleRepository : Repository<TempleMaster>, ITempleRepository
    {
        private readonly TempleDbContext _context;
        public TempleRepository(TempleDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }        

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.TempleMasters
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TempleMaster?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.TempleMasters
                .Include(t => t.TemplePoojas)
                .Include(t => t.Donations)
                .Include(t => t.Prasads)
                .Include(t => t.Aartis)
                .Include(t => t.TempleSchedules)
                .Include(t => t.TempleExceptions)
                .Include(t => t.Localizations)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}