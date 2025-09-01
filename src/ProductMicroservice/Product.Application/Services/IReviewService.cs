
using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface IReviewService
    {
        Task<ReviewSummaryDto> GetProductReviewSummaryAsync(int pid);
    }
}