using System.ComponentModel.DataAnnotations;
using Payment.Domain.Enums;
using PaymentMicroservice.Domain.Enums;

namespace PaymentMicroservice.Domain.Entities
{
    public class PaymentTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string PaymentReference { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string? OrderId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, MaxLength(10)]
        public string Currency { get; set; } = "INR";

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [MaxLength(500)]
        public string? StatusMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        // Navigation
        public virtual PaymentMethodDetails PaymentMethodDetails { get; set; } = null!;
        public virtual ICollection<PaymentTransactionLog> TransactionLogs { get; set; } = new List<PaymentTransactionLog>();
        public virtual ICollection<RefundTransaction> Refunds { get; set; } = new List<RefundTransaction>();
    }

}