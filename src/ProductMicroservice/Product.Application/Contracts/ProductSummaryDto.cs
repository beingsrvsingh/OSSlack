
namespace Product.Application.Contracts
{
    public class ProductSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? ImageUrl { get; set; }
        public double Cost { get; set; } = 0.0;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int Rating { get; set; }
        public int Reviews { get; set; }
        public int Quantity { get; set; } = 1;
        public int Limit { get; set; } = 1;
    }

}