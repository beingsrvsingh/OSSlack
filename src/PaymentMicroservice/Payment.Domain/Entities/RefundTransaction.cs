
using System.ComponentModel.DataAnnotations;
using PaymentMicroservice.Domain.Enums;

namespace PaymentMicroservice.Domain.Entities
{
    public class RefundTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentTransactionId { get; set; }

        [Required]
        public PaymentTransaction PaymentTransaction { get; set; } = null!;

        [Required]
        public decimal RefundAmount { get; set; }

        [Required]
        public RefundStatus Status { get; set; } // Enum: Pending, Success, Failed

        [MaxLength(500)]
        public string? Reason { get; set; } // Refund reason

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
    }

}