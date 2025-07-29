using Review.Domain.Entities;
using Shared.Domain.Repository;

namespace Review.Domain.Core.Repository
{
    public interface IReviewReportRepository : IRepository<ReviewReport>
    {
        Task<List<ReviewReport>> GetPendingReportsByReviewIdAsync(int reviewId);
        Task<(List<ReviewReport> Items, int TotalCount)> GetPendingReportsAsync(int page = 1, int pageSize = 20);

    }
}