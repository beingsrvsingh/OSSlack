namespace Order.Application.Contracts
{
    public class OrderResponse
    {
        public string OrderNumber { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
