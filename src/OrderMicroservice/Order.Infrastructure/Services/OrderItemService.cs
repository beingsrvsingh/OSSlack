using Order.Application.Contracts;
using Order.Application.Services;
using Order.Domain.Core.Repository;

namespace Order.Infrastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductService productService;

        public OrderItemService(IOrderItemRepository orderItemRepository, IProductService productService)
        {
            _orderItemRepository = orderItemRepository;
            this.productService = productService;
        }

        public async Task<List<TrendingProduct>> GetTrendingProductsByCategoryAsync(int categoryId, int topN = 5)
        {
            var fromDate = DateTime.UtcNow.AddDays(-30);

            // 1. Get recent order items
            var recentOrderItems = await _orderItemRepository.GetTrendingProductsByCategoryAsync(fromDate);

            var productIds = recentOrderItems.Select(oi => oi.ProductId).Distinct().ToList();

            // 2. Get product info from catalog microservice
            var products = await productService.GetProductsByIdAndCategoryIdAsync(productIds, categoryId);

            // 4. Filter order items for filtered products
            var filteredOrderItems = recentOrderItems
                .Where(oi => products.Any(p => p.Id == oi.ProductId))
                .ToList();

            // 5. Aggregate trending data
            var productMap = products.ToDictionary(p => p.Id);

            var trendingProducts = filteredOrderItems
                .GroupBy(oi => oi.ProductId)
                .Where(g => productMap.ContainsKey(g.Key))
                .Select(g =>
                {
                    var product = productMap[g.Key];
                    return new TrendingProduct
                    {   
                        ProductId = g.Key,
                        ProductName = product.Name
                    };
                })
                .ToList();


            return trendingProducts;
        }
    }
}