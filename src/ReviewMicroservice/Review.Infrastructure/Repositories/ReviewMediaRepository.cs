using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReviewMediaRepository(ReviewDbContext dbContext) : Repository<ReviewMedia>(dbContext), IReviewMediaRepository
    {
        private readonly ReviewDbContext dbContext = dbContext;

        public async Task<List<ReviewMedia>> GetByReviewIdAsync(int reviewId)
        {
            return await this.dbContext.ReviewMedia
                .Where(m => m.ReviewId == reviewId)
                .ToListAsync();
        }

    }
}
