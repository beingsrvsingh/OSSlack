using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Persistence.EntityConfigurations
{
    public class StockAlertConfiguration : IEntityTypeConfiguration<StockAlert>
    {
        public void Configure(EntityTypeBuilder<StockAlert> builder)
        {
            builder.ToTable("stock_alert");

            builder.HasKey(sa => sa.Id).HasName("pk_stock_alert");

            builder.Property(sa => sa.Id).HasColumnName("id");
            builder.Property(sa => sa.StockId).HasColumnName("stock_id");
            builder.Property(sa => sa.CurrentQuantity).HasColumnName("current_quantity");
            builder.Property(sa => sa.Threshold).HasColumnName("threshold");
            builder.Property(sa => sa.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            builder.Property(sa => sa.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(sa => sa.Stock)
                .WithMany()
                .HasForeignKey(sa => sa.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}