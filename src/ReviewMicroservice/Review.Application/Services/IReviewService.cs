using Review.Application.Contracts;
using Review.Application.Features.Commands;
using Review.Application.Features.Queries;
using Review.Domain.Entities;

using ReviewEntity = Review.Domain.Entities.Review;

namespace Review.Application.Services
{
    public interface IReviewService
    {
        Task<List<ReviewReportReasonDto>> GetReportReasonsAsync();

        Task<ReviewDto?> GetByReviewIdAsync(int reviewId);

        Task<ReviewEntity?> GetEntityByIdAsync(int reviewId);

        Task<bool> AddReviewAsync(AddReviewCommand command);

        Task<bool> UpdateReviewAsync(UpdateReviewCommand command);

        Task<bool> DeleteAsync(ReviewDto review);

        Task<bool> ReportReviewAsync(ReviewReportCommand command);

        Task<bool> MarkReviewHelpfulAsync(ReviewFeedbackCommand command);

        Task<bool> HasUserMarkedReviewHelpfulAsync(int reviewId, string userId);

        Task<bool> SupportResolveReportAsync(SupportResolveReportCommand command);

        Task<bool> ModerateReviewAsync(ReviewModerationCommand command);

        Task<ReviewSummaryDto> GetProductReviewSummaryAsync(GetProductReviewSummaryQuery query);

        Task<List<ReviewSummaryDto>> GetProductReviewSummariesAsync(List<int> productIds);

        Task<(List<ReportedReviewDto> Items, int TotalCount)> GetReportedReviewsAsync(int page, int pageSize);

        Task<(List<ReviewDto> Items, int TotalCount)> GetReviewsByProductAsync(GetReviewsByProductQuery query);

        Task<(List<ReviewDto> Items, int TotalCount)> GetReviewsByUserAsync(GetReviewsByUserQuery query);
    }
}
