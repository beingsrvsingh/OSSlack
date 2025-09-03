
namespace Product.Domain.Entities
{
    public class ProductFilterRawResult
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public string? ThumbnailUrl { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string? AttributeKey { get; set; }
        public string? AttributeLabel { get; set; }
        public string? Value { get; set; }
    }

}