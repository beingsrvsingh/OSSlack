using Review.Application.Services;
using Review.Domain.Entities;
using Review.Domain.Repository;

namespace Review.Infrastructure.Services
{
    public class ReviewDetailService(IReviewDetailRepository repository) : IReviewDetailService
    {
        private readonly IReviewDetailRepository repository = repository;

        public async Task<ReviewDetail?> GetByAsync(int reviewId, int productId, string userId)
        {
            return await repository.GetBy(r => r.ReviewId == reviewId && r.UserId == userId && r.ProductId == productId);
        }

        public void AddAsync(ReviewDetail entities)
        {
            repository.AddAsync(entities);
            repository.Save();
        }        

        public void UpdateAsync(ReviewDetail entities)
        {
            repository.UpdateAsync(entities);
            repository.Save();
        }
    }
}
