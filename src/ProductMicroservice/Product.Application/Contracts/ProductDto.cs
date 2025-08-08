
namespace Product.Application.Contracts
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public IEnumerable<ProductVariantDto>? Variants { get; set; }
    }


}