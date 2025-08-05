using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}