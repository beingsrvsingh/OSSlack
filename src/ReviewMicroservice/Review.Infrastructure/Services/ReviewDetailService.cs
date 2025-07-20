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

        public async Task AddAsync(ReviewDetail entities)
        {
            repository.AddAsync(entities);
            await repository.SaveChangesAsync();
        }        

        public async Task UpdateAsync(ReviewDetail entities)
        {
            await repository.UpdateAsync(entities);
            await repository.SaveChangesAsync();
        }
    }
}
