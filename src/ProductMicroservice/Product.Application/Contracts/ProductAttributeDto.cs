
namespace Product.Application.Contracts
{
    public class ProductAttributeDto
    {
        public int Id { get; set; }
        public string AttributeName { get; set; } = null!;
        public string AttributeValue { get; set; } = null!;
    }

}