
namespace PaymentMicroservice.Application.Contracts
{
    public class RefundTransactionDto
    {
        public int Id { get; set; }
        public int PaymentTransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; } = null!;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
    }

}