using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReviewFeedbackRepository(ReviewDbContext dbContext) : Repository<ReviewFeedback>(dbContext), IReviewFeedbackRepository
    {
        private readonly ReviewDbContext dbContext = dbContext;

        public async Task<bool> HasUserMarkedFeedbackAsync(int reviewId, string userId)
        {
            return await this.dbContext.ReviewFeedbacks.AnyAsync(f => f.ReviewId == reviewId && f.UserId == userId);
        }

        public async Task<ReviewFeedback?> GetFeedbackByReviewAndUserAsync(int reviewId, string userId)
        {
            return await this.dbContext.ReviewFeedbacks
                .FirstOrDefaultAsync(f => f.ReviewId == reviewId && f.UserId == userId);
        }

    }
}
