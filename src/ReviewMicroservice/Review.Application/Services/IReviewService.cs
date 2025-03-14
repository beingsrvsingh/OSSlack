using Review.Domain.Entities;

namespace Review.Application.Services
{
    public interface IReviewService : IBaseService<Review.Domain.Entities.Reviews>
    {
        Task<IReadOnlyList<Review.Domain.Entities.Reviews>> GetReviewByProduct(int productId, int totalRecords = 0, int takeRecords = 10);
        Task<Reviews?> GetByAsync(int reviewId, int productId, string userId);
    }    
}
