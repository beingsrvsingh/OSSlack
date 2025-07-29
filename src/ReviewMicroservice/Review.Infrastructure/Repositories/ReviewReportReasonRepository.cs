using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReviewReportReasonRepository : Repository<ReviewReportReason>, IReviewReportReasonRepository
    {
        private readonly ReviewDbContext dbContext;

        public ReviewReportReasonRepository(ReviewDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ReviewReportReason>> GetAllByDisplayOrderAsync()
        {
            return await dbContext.ReportReasons
                .OrderBy(r => r.DisplayOrder)
                .ToListAsync();
        }
    }
}
