using Review.Domain.Entities;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReviewDetailRepository : Repository<ReviewDetail>, IReviewDetailRepository
    {
        private readonly ReviewDbContext dbContext;

        public ReviewDetailRepository(ReviewDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
