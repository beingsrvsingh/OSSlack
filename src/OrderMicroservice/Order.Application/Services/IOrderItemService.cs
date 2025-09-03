using Order.Application.Contracts;

namespace Order.Application.Services
{
    public interface IOrderItemService
    {
        Task<List<TrendingProduct>> GetTrendingProductsByCategoryAsync(int categoryId, int topN = 5);

    }
}