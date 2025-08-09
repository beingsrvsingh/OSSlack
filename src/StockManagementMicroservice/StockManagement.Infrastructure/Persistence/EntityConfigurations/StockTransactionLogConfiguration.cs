using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Persistence.EntityConfigurations
{
    public class StockTransactionLogConfiguration : IEntityTypeConfiguration<StockTransactionLog>
    {
        public void Configure(EntityTypeBuilder<StockTransactionLog> builder)
        {
            builder.ToTable("stock_transaction_log");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.StockTransactionLogId)
                   .IsRequired()
                   .HasColumnName("stock_id");

            builder.Property(e => e.ChangeQuantity)
                   .HasColumnName("change_quantity");

            builder.Property(e => e.TransactionType)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("transaction_type");

            builder.Property(e => e.ReferenceId)
                   .HasColumnName("reference_id");

            builder.Property(e => e.CreatedOn)
                   .HasColumnName("created_on")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            // No navigation property for Stock to keep archival table lightweight
        }
    }

}