using Microsoft.EntityFrameworkCore;
using Review.Domain.Enum;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Domain.Contracts;
using Shared.Infrastructure.Repositories;

using ReviewEntity = Review.Domain.Entities.Review;

namespace Review.Infrastructure.Repositories
{
    public class ReviewRepository(ReviewDbContext dbContext) : Repository<ReviewEntity>(dbContext), IReviewRepository
    {
        private readonly ReviewDbContext dbContext = dbContext;

        public async Task<ReviewEntity?> GetByUserAndProductAsync(string userId, int productId)
        {
            return await this.dbContext.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public async Task<ReviewEntity?> GetReviewWithDetailsAsync(int reviewId)
        {
            var review = await this.dbContext.Reviews
                            .Include(r => r.Feedbacks)
                            .Include(r => r.Medias)
                            .Include(r => r.Reports)
                            .FirstOrDefaultAsync(r => r.Id == reviewId);

            return review;
        }

        public async Task<List<ReviewEntity>> GetActiveReviewsByProductIdAsync(int productId)
        {
            return await this.dbContext.Reviews
                .Where(r => r.ProductId == productId && r.Status == ReviewStatus.Active)
                .ToListAsync();
        }

        public async Task<PaginatedResult<ReviewEntity>> GetPaginatedByProductIdAsync(int productId, int page, int pageSize)
        {
            var query = this.dbContext.Reviews.Where(r => r.ProductId == productId);

            var totalCount = await query.CountAsync();

            var items = await query.Include(f=>f.Feedbacks).Include(r => r.Medias).Take(3)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<ReviewEntity>(
            items,
            totalCount,
            page,
            pageSize,
            (int)Math.Ceiling(totalCount / (double)pageSize));
        }

        public async Task<PaginatedResult<ReviewEntity>> GetPaginatedByUserIdAsync(string userId, int page, int pageSize)
        {
            var query = dbContext.Reviews.Where(r => r.UserId == userId);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResult<ReviewEntity>(items, totalCount, page, pageSize, totalPages);
        }

    }
}
