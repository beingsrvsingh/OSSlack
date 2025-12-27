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

            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(pm => pm.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18,2)");

                price.Property(pm => pm.Mrp)
                    .HasColumnName("mrp")
                    .HasColumnType("decimal(18,2)");

                price.Property(pm => pm.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("currency");

                price.Property(pm => pm.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(18,2)");

                price.Property(pm => pm.Tax)
                    .HasColumnName("tax")
                    .HasColumnType("decimal(18,2)");

                price.Property(pm => pm.EffectiveFrom)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                    .HasColumnName("price_effective_from");

                price.Property(pm => pm.EffectiveTo)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                    .HasColumnName("price_effective_to");
            });

            builder.Property(v => v.StockQuantity)
                .HasColumnName("stock_quantity");

            builder.Property(v => v.IsDefault)
                .HasColumnName("is_default");

            builder.Property(v => v.ProductMasterId)
                .HasColumnName("product_master_id");

            builder.HasOne(v => v.ProductMaster)
                .WithMany(p => p.VariantMasters)   // navigation property in ProductMaster
                .HasForeignKey(v => v.ProductMasterId) // matches the FK property
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships for VariantImages
            builder.HasMany(v => v.Media)
                .WithOne(vi => vi.ProductVariant)
                .HasForeignKey(vi => vi.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships for Attributes
            builder.HasMany(v => v.Attributes)
                .WithOne(a => a.ProductVariant)
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Addons)
                .WithOne(a => a.ProductVariantMaster)
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}