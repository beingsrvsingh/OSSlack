using Microsoft.EntityFrameworkCore;
using Review.Domain.Core.Repository;
using Review.Domain.Entities;
using Review.Domain.Enum;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReviewReportRepository : Repository<ReviewReport>, IReviewReportRepository
    {
        private readonly ReviewDbContext dbContext;

        public ReviewReportRepository(ReviewDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ReviewReport>> GetPendingReportsByReviewIdAsync(int reviewId)
        {
            return await this.dbContext.ReviewReports
                .Where(r => r.ReviewId == reviewId && r.Status == ReportStatus.Pending)
                .ToListAsync();
        }

        public async Task<(List<ReviewReport> Items, int TotalCount)> GetPendingReportsAsync(int page = 1, int pageSize = 20)
        {
            return await GetPaginatedAsync(
                r => r.Status == ReportStatus.Pending,
                q => q.OrderByDescending(r => r.ReportedAt),
                page,
                pageSize);
        }

    }
}
