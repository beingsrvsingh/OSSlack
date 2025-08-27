
namespace Product.Application.Contracts
{
    public class ProductAttributeGroupDto
    {
        public string GroupName { get; set; } = "Basic Details";
        public List<ProductAttributeDto> Attributes { get; set; } = new();
    }

}