
namespace Product.Application.Contracts
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string VariantName { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }

}