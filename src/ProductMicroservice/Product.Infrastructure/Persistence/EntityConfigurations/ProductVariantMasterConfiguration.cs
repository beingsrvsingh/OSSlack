using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductVariantMasterConfiguration : IEntityTypeConfiguration<ProductVariantMaster>
    {
        public void Configure(EntityTypeBuilder<ProductVariantMaster> builder)
        {
            builder.ToTable("product_variant_master");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(v => v.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            builder.Property(v => v.MRP)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("mrp");

            builder.Property(v => v.StockQuantity)
                .HasColumnName("stock_quantity");

            builder.Property(v => v.IsDefault)
                .HasColumnName("is_default");

            builder.Property(v => v.DurationMinutes)
            .HasColumnType("int")
            .IsRequired(false);

            builder.Property(v => v.AvailableSlots)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(v => v.BookingType)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(v => v.ProductMasterId)
                .HasColumnName("product_master_id");

            builder.HasOne(v => v.ProductMaster)
                .WithMany(p => p.VariantMasters)   // navigation property in ProductMaster
                .HasForeignKey(v => v.ProductMasterId) // matches the FK property
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships for VariantImages
            builder.HasMany(v => v.VariantImages)
                .WithOne(vi => vi.ProductVariant)
                .HasForeignKey(vi => vi.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships for Attributes
            builder.HasMany(v => v.Attributes)
                .WithOne(a => a.ProductVariant)
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}