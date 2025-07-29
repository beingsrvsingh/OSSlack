using Review.Domain.Entities;
using Shared.Domain.Repository;

namespace Review.Domain.Repository
{
    public interface IReviewReportReasonRepository : IRepository<ReviewReportReason>
    {
        Task<List<ReviewReportReason>> GetAllByDisplayOrderAsync();
    }
}
