
namespace Product.Application.Contracts
{
    public class ProductRegionPriceDto
{
    public int Id { get; set; }
    public string Region { get; set; } = null!;
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
}

}