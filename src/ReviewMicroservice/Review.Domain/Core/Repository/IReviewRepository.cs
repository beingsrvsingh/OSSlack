using Shared.Domain.Contracts;
using Shared.Domain.Repository;

using ReviewEntity = Review.Domain.Entities.Review;

namespace Review.Domain.Repository
{
    public interface IReviewRepository : IRepository<ReviewEntity>
    {
        Task<ReviewEntity?> GetByUserAndProductAsync(string userId, int productId);
        Task<ReviewEntity?> GetReviewWithDetailsAsync(int reviewId);
        Task<List<ReviewEntity>> GetActiveReviewsByProductIdAsync(int productId);
        Task<PaginatedResult<ReviewEntity>> GetPaginatedByProductIdAsync(int productId, int page, int pageSize);
         Task<PaginatedResult<ReviewEntity>> GetPaginatedByUserIdAsync(string userId, int page, int pageSize);
    }
}
