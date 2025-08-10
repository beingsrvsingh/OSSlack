
using System.ComponentModel.DataAnnotations;

namespace PaymentMicroservice.Domain.Entities
{
    public class PaymentMethodDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentTransactionId { get; set; }

        public PaymentTransaction PaymentTransaction { get; set; } = null!;

        [MaxLength(100)]
        public string? CardHolderName { get; set; }

        [MaxLength(20)]
        public string? MaskedCardNumber { get; set; } // e.g., **** **** **** 1234

        [MaxLength(50)]
        public string? WalletId { get; set; } // If payment by wallet

        [MaxLength(50)]
        public string? BankName { get; set; } // If net banking

        // Add more fields as needed based on payment method
    }

}