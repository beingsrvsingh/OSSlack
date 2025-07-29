using Review.Domain.Entities;
using Shared.Domain.Repository;


namespace Review.Domain.Repository
{
    public interface IReviewFeedbackRepository : IRepository<ReviewFeedback>
    {
        Task<bool> HasUserMarkedFeedbackAsync(int reviewId, string userId);
        Task<ReviewFeedback?> GetFeedbackByReviewAndUserAsync(int reviewId, string userId);

    }
}
