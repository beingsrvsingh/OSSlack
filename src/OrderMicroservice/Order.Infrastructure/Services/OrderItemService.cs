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

            var productIds = recentOrderItems.Select(oi => oi.ProductVariantId).Distinct().ToList();

            // 2. Get product info from catalog microservice
            var products = await productService.GetProductsByIdAndCategoryIdAsync(productIds, categoryId);

            // 4. Filter order items for filtered products
            var filteredOrderItems = recentOrderItems
                .Where(oi => products.Any(p => p.Id == oi.ProductVariantId))
                .ToList();

            // 5. Aggregate trending data
            var productMap = products.ToDictionary(p => p.Id);

            var trendingProducts = filteredOrderItems
                                .GroupBy(oi => oi.ProductVariantId)
                                .Where(g => productMap.ContainsKey(g.Key))
                                .Select(g =>
                                {
                                    var product = productMap[g.Key];

                                    return new TrendingProduct
                                    {
                                        Pid = product.Id.ToString(),
                                        Cid = product.CategoryId.ToString(),
                                        Scid = product.SubCategoryId.ToString(),
                                        Name = product.Name,
                                        ThumbnailUrl = product.ImageUrl,
                                        Cost = (double)product.Cost,
                                        Rating = product.Rating,
                                        Reviews = product.Reviews,
                                        Quantity = 1,
                                        Limit = 1
                                    };
                                })
                                .ToList();


            return trendingProducts;
        }
    }
}