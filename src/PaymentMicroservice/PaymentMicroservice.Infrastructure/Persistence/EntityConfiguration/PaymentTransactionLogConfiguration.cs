using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentMicroservice.Domain.Entities;

namespace PaymentMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    public class PaymentTransactionLogConfiguration : IEntityTypeConfiguration<PaymentTransactionLog>
    {
        public void Configure(EntityTypeBuilder<PaymentTransactionLog> builder)
        {
            builder.ToTable("payment_transaction_log");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentTransactionId)
                .IsRequired()
                .HasColumnName("payment_transaction_id");

            builder.Property(x => x.Timestamp)
                .HasColumnName("timestamp");

            builder.Property(x => x.EventType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("event_type");

            builder.Property(x => x.Message)
                .HasMaxLength(1000)
                .HasColumnName("message");
        }
    }

}