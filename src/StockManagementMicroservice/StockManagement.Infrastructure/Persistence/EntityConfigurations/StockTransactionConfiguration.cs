using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Persistence.EntityConfigurations
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.ToTable("stock_transaction");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.StockId)
                   .IsRequired()
                   .HasColumnName("stock_id");

            builder.Property(e => e.QuantityChanged)
                   .HasColumnName("quantity_changed");

            builder.Property(e => e.ActionType)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("action_type");

            builder.Property(e => e.Notes)
                   .HasMaxLength(250)
                   .HasColumnName("notes");

            builder.Property(e => e.Timestamp)
                   .HasColumnName("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(e => e.Stock)
                   .WithMany()
                   .HasForeignKey(e => e.StockId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("fk_stocktransaction_stock");
        }
    }
}