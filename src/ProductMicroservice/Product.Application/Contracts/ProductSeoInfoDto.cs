
namespace Product.Application.Contracts
{
    public class ProductSeoInfoDto
    {
        public int Id { get; set; }
        public string MetaTitle { get; set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }

}