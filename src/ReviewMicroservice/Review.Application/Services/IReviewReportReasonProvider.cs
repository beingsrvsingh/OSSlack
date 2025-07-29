using Review.Domain.Entities;

namespace Review.Application.Services
{
    public interface IReviewReportReasonProvider
    {
        IReadOnlyList<ReviewReportReason> GetDefaultReasons();
    }

}