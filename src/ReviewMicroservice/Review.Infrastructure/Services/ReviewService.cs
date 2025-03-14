using Review.Application.Services;
using Review.Domain.Entities;
using Review.Domain.Repository;
using System.Linq.Expressions;

namespace Review.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository repository;

        public ReviewService(IReviewRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Reviews?> GetByAsync(int reviewId, int productId, string userId)
        {
            return await repository.GetBy(r => r.Id == reviewId && r.UserId == userId && r.ProductId == productId);
        }

        public void AddAsync(Reviews entities)
        {
            repository.AddAsync(entities);
            repository.Save();
        }

        public void UpdateAsync(Reviews entities)
        {
            repository.UpdateAsync(entities);
            repository.Save();
        }

        public async Task<IReadOnlyList<Reviews>> GetReviewByProduct(int productId, int totalRecords = 0, int takeRecords = 10)
        {
            var includesEntity = new List<Expression<Func<Reviews, Object>>>();
            includesEntity.Add(r => r.ReviewDetails);
            return await repository.GetAsync(x => x.ProductId == productId, x => x.OrderByDescending(y => y.CreatedDate), includesEntity);
        }
    }
}
