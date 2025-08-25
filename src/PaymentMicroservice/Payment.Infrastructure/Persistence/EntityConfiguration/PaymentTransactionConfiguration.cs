using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentMicroservice.Domain.Entities;

namespace PaymentMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("payment_transaction");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentReference)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("payment_reference");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("user_id");

            builder.Property(x => x.OrderId)
                .IsRequired()
                .HasColumnName("order_id");

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("amount");

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("currency");

            builder.Property(x => x.PaymentMethod)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("payment_method");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("status");

            builder.Property(x => x.StatusMessage)
                .HasMaxLength(500)
                .HasColumnName("status_message");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(x => x.CompletedAt)
                .HasColumnName("completed_at");

            // One-to-many: TransactionLogs
            builder.HasMany(x => x.TransactionLogs)
                .WithOne(x => x.PaymentTransaction)
                .HasForeignKey(x => x.PaymentTransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: Refunds
            builder.HasMany(x => x.Refunds)
                .WithOne(x => x.PaymentTransaction)
                .HasForeignKey(x => x.PaymentTransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one: PaymentMethodDetails
            builder.HasOne(x => x.PaymentMethodDetails)
                .WithOne(x => x.PaymentTransaction)
                .HasForeignKey<PaymentMethodDetails>(x => x.PaymentTransactionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}