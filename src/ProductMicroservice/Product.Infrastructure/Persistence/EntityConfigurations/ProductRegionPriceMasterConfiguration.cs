using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductRegionPriceMasterConfiguration : IEntityTypeConfiguration<ProductRegionPriceMaster>
    {
        public void Configure(EntityTypeBuilder<ProductRegionPriceMaster> builder)
        {
            builder.ToTable("product_region_price_master");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id");

            builder.Property(r => r.RegionCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("region_code");

            builder.Property(r => r.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            builder.Property(r => r.ProductId)
                .HasColumnName("product_id");

            builder.HasOne(r => r.ProductMaster)
                .WithMany(p => p.RegionPriceMaster)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}