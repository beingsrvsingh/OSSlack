using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly DbContext dbContext;

        public ScheduleRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
