using Review.Domain.Entities;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReportLookUpRepository : Repository<ReviewReportLookup>, IReportLookUpRepository
    {
        private readonly ReviewDbContext dbContext;

        public ReportLookUpRepository(ReviewDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
