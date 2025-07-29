using Review.Domain.Entities;
using Shared.Domain.Repository;

namespace Review.Domain.Repository
{
    public interface IReviewMediaRepository : IRepository<ReviewMedia>
    { 
        Task<List<ReviewMedia>> GetByReviewIdAsync(int reviewId);
    }
}
