
namespace Order.Application.Contracts
{

    public class TrendingProduct
    {
        public string Pid { get; set; } = "";
        public string Cid { get; set; } = "";
        public string Scid { get; set; } = "";
        public string Name { get; set; } = "";
        public string? ThumbnailUrl { get; set; }
        public double Cost { get; set; }
        public int Rating { get; set; }
        public int Reviews { get; set; }
        public string CategoryType { get; set; } = "";
        public int Quantity { get; set; } = 1;
        public int Limit { get; set; } = 1;

        public List<Object> Attributes { get; set; } = new();
    }
}