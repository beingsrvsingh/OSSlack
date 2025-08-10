
using System.ComponentModel.DataAnnotations;

namespace PaymentMicroservice.Domain.Entities
{
    public class PaymentTransactionLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentTransactionId { get; set; }

        [Required]
        public PaymentTransaction PaymentTransaction { get; set; } = null!;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(100)]
        public string EventType { get; set; } = null!; // e.g. "PaymentInitiated", "PaymentSuccess", "PaymentFailed"

        [MaxLength(1000)]
        public string? Message { get; set; } // Additional details or gateway response payload
    }

}