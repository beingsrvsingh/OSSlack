using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagementMicroservice.Domain.Entities;

namespace StockManagementMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class WarehouseMasterConfiguration : IEntityTypeConfiguration<WarehouseMaster>
    {
        public void Configure(EntityTypeBuilder<WarehouseMaster> builder)
        {
            builder.ToTable("warehouse_master");

            builder.HasKey(w => w.Id).HasName("pk_warehouse_master");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(w => w.Address)
                .HasMaxLength(200)
                .HasColumnName("address");

            builder.Property(w => w.PostalCode)
                .HasMaxLength(50)
                .HasColumnName("region_code");

            builder.Property(w => w.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.HasMany(w => w.Stocks)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}