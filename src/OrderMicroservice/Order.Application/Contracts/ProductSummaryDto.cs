
namespace Order.Application.Contracts
{
    public class ProductSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }

}