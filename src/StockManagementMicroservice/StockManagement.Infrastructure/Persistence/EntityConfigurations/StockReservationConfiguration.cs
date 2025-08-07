using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Persistence.EntityConfigurations
{
    public class StockReservationConfiguration : IEntityTypeConfiguration<StockReservation>
    {
        public void Configure(EntityTypeBuilder<StockReservation> builder)
        {
            builder.ToTable("stock_reservation");

            builder.HasKey(sr => sr.Id).HasName("pk_stock_reservation");

            builder.Property(sr => sr.Id).HasColumnName("id");
            builder.Property(sr => sr.StockId).HasColumnName("stock_id");
            builder.Property(sr => sr.Quantity).HasColumnName("quantity");
            builder.Property(sr => sr.ReservedBy)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("reserved_by");
            builder.Property(sr => sr.ReservedAt)
                .HasColumnName("reserved_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(sr => sr.ExpiresAt).HasColumnName("expires_at");

            builder.HasOne(sr => sr.Stock)
                .WithMany()
                .HasForeignKey(sr => sr.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}