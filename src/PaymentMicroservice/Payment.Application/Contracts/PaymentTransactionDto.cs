

namespace PaymentMicroservice.Application.Contracts
{
    public class PaymentTransactionDto
    {
        public required string OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
    }

}