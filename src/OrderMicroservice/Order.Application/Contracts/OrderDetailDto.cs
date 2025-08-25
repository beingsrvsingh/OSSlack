
using Shared.Application.Contracts;

namespace Order.Application.Contracts
{
    public class OrderDetailDto
    {
        public OrderSummaryDto OrderSummaryDto { get; set; } = null!;
        public PaymentInfoDto PaymentInfo { get; set; } = null!;
        public BillDetailsDto BillDetails { get; set; } = null!;
        public ShippingInfoDto ShippingInfoDto { get; set; } = null!;
    }

}