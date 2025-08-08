
namespace Order.Application.Contracts
{
    public class OrderItemCustomizationDto
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }
        public string CustomizationType { get; set; } = null!;
        public string CustomizationValue { get; set; } = null!;
    }
}