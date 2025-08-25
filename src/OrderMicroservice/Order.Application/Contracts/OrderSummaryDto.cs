
namespace Order.Application.Contracts
{
    public class OrderSummaryDto
    {
        public string OrderNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal ConvenienceFee { get; set; }
        public string Url { get; set; } = null!;
    }

}