using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMicroservice.Domain.Entities;

namespace StockMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class StockAdjustmentConfiguration : IEntityTypeConfiguration<StockAdjustment>
    {
        public void Configure(EntityTypeBuilder<StockAdjustment> builder)
        {
            builder.ToTable("stock_adjustment");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.StockId)
                .IsRequired()
                .HasColumnName("stock_id");

            builder.Property(e => e.AdjustmentType)
                .IsRequired()
                .HasColumnName("adjustment_type")
                .HasConversion<string>()   // Store enum as string

                .HasMaxLength(50);

            builder.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<string>()   // Store enum as string

                .HasMaxLength(50);

            builder.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");

            builder.Property(e => e.AdjustmentDate)
                .IsRequired()
                .HasColumnName("adjustment_date");

            builder.HasOne(e => e.Stock)
                .WithMany()
                .HasForeignKey(e => e.StockId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_stock_adjustment_stock");
        }
    }
}