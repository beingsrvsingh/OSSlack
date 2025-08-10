
namespace PaymentMicroservice.Application.Contracts
{
    public class PaymentTransactionLogDto
    {
        public int Id { get; set; }
        public int PaymentTransactionId { get; set; }
        public string Message { get; set; } = null!;
        public string Level { get; set; } = null!;
        public DateTime LogTime { get; set; }
    }

}