using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentMicroservice.Domain.Entities;

namespace PaymentMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    public class PaymentMethodDetailsConfiguration : IEntityTypeConfiguration<PaymentMethodDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodDetails> builder)
        {
            builder.ToTable("payment_method_details");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentTransactionId)
                .IsRequired()
                .HasColumnName("payment_transaction_id");

            builder.Property(x => x.CardHolderName)
                .HasMaxLength(100)
                .HasColumnName("card_holder_name");

            builder.Property(x => x.MaskedCardNumber)
                .HasMaxLength(20)
                .HasColumnName("masked_card_number");

            builder.Property(x => x.WalletId)
                .HasMaxLength(50)
                .HasColumnName("wallet_id");

            builder.Property(x => x.BankName)
                .HasMaxLength(50)
                .HasColumnName("bank_name");

            builder.Property(x => x.UpiId)
                .HasMaxLength(50)
                .HasColumnName("upi_id");

            builder.Property(x => x.CardType)
                .HasMaxLength(50)
                .HasColumnName("card_type");

            // One-to-one relationship with PaymentTransaction
            builder.HasOne(x => x.PaymentTransaction)
                .WithOne(p => p.PaymentMethodDetails) // <- navigation from PaymentTransaction
                .HasForeignKey<PaymentMethodDetails>(x => x.PaymentTransactionId)
                .OnDelete(DeleteBehavior.Cascade); // optional: adjust as needed
        }
    }
}