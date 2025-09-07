using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakTimeSlotRepository : Repository<KathavachakTimeSlot>, IKathavachakTimeSlotRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakTimeSlotRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }

}
