
using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface IReviewService
    {
        Task<ReviewSummaryDto> GetProductReviewSummaryAsync(int pid);
        Task<List<ReviewSummaryDto>> GetProductReviewSummariesAsync(List<int> pids);
    }
}