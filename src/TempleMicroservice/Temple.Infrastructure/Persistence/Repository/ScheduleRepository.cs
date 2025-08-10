using Temple.Domain.Entities;
using Temple.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}