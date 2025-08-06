using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMicroservice.Domain.Entities;

namespace StockMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class StockMasterConfiguration : IEntityTypeConfiguration<StockMaster>
    {
        public void Configure(EntityTypeBuilder<StockMaster> builder)
        {
            builder.ToTable("stock_master");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.ProductId).IsRequired().HasColumnName("product_id");

            builder.Property(e => e.WarehouseId).IsRequired().HasColumnName("warehouse_id");

            builder.Property(e => e.QuantityAvailable).IsRequired().HasColumnName("quantity_available");

            builder.Property(e => e.QuantityReserved).IsRequired().HasDefaultValue(0).HasColumnName("quantity_reserved");

            builder.Property(e => e.QuantityDamaged).IsRequired().HasDefaultValue(0).HasColumnName("quantity_damaged");

            builder.Property(e => e.LastUpdatedAt).HasColumnName("last_updated_at");

            builder.HasOne(e => e.Warehouse)
                   .WithMany()
                   .HasForeignKey(e => e.WarehouseId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("fk_stockmaster_warehouse");
        }
    }

}