
namespace Order.Application.Contracts
{
    public class TrendingProductDto : TrendingProduct
    {
        public string? ProductImageUrl { get; set; }
    }

    public class TrendingProduct{
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
    }

}