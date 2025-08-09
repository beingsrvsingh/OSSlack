using System.ComponentModel.DataAnnotations;
using PaymentMicroservice.Domain.Enums;

namespace PaymentMicroservice.Domain.Entities
{
    public class PaymentTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string PaymentReference { get; set; } = null!; // Unique identifier for payment gateway

        public required string UserId { get; set; }

        [Required]
        public int OrderId { get; set; } // Reference to order in Order MS

        [Required]
        public decimal Amount { get; set; }

        [Required, MaxLength(10)]
        public string Currency { get; set; } = "USD";

        [Required]
        public PaymentMethod PaymentMethod { get; set; } // Enum: CreditCard, Wallet, NetBanking, UPI etc.

        [Required]
        public PaymentStatus Status { get; set; } // Enum: Pending, Success, Failed, Refunded, Cancelled

        [MaxLength(500)]
        public string? StatusMessage { get; set; } // Description or error message from gateway

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        // Navigation
        public virtual ICollection<PaymentTransactionLog> TransactionLogs { get; set; } = new List<PaymentTransactionLog>();

        public virtual ICollection<RefundTransaction> Refunds { get; set; } = new List<RefundTransaction>();
    }

}