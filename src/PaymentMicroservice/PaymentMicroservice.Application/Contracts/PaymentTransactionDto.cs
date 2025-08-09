

namespace PaymentMicroservice.Application.Contracts
{
    public class PaymentTransactionDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}