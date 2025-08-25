
namespace Order.Application.Contracts
{
    public class BillDetailsDto
    {
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal HandlingCharge { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal Total { get; set; }
    }

}