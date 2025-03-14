using Review.Domain.Entities;

namespace Review.Application.Services
{
    public interface IReviewDetailService : IBaseService<ReviewDetail>
    {
        Task<ReviewDetail?> GetByAsync(int reviewId, int productId, string userId);
    }
}
