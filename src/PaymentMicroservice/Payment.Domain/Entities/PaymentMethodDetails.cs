using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Payment.Domain.Enums;

namespace PaymentMicroservice.Domain.Entities
{
    public class PaymentMethodDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentTransactionId { get; set; }

        [ForeignKey(nameof(PaymentTransactionId))]
        public virtual PaymentTransaction PaymentTransaction { get; set; } = null!;

        [MaxLength(100)]
        public string? CardHolderName { get; set; }

        [MaxLength(20)]
        public string? MaskedCardNumber { get; set; }

        [MaxLength(50)]
        public string? WalletId { get; set; }

        [MaxLength(50)]
        public string? BankName { get; set; }

        [MaxLength(100)]
        public string? UpiId { get; set; } // NEW: for UPI payments

        public CardType? CardType { get; set; } // Optional enum field (Visa, MasterCard, etc.)
    }
}