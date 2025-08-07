using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentMicroservice.Domain.Entities;

namespace PaymentMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    public class RefundTransactionConfiguration : IEntityTypeConfiguration<RefundTransaction>
    {
        public void Configure(EntityTypeBuilder<RefundTransaction> builder)
        {
            builder.ToTable("refund_transaction");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentTransactionId)
                .IsRequired()
                .HasColumnName("payment_transaction_id");

            builder.Property(x => x.RefundAmount)
                .IsRequired()
                .HasColumnName("refund_amount");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("status");

            builder.Property(x => x.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");

            builder.Property(x => x.RequestedAt)
                .HasColumnName("requested_at");

            builder.Property(x => x.ProcessedAt)
                .HasColumnName("processed_at");
        }
    }

}