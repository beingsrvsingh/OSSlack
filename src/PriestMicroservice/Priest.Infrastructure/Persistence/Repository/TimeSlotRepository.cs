using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class TimeSlotRepository : Repository<TimeSlot>, ITimeSlotRepository
    {
        private readonly DbContext dbContext;

        public TimeSlotRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
