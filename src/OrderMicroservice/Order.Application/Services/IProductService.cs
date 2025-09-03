
using Order.Application.Contracts;

namespace Order.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductSummaryDto>> GetProductsByIdAndCategoryIdAsync(List<int> pids, int cid);
    }
}