using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Review.Infrastructure.Repositories
{
    public class ReviewRepository(ReviewDbContext dbContext) : Repository<Domain.Entities.Reviews>(dbContext), IReviewRepository
    {
    }
}
