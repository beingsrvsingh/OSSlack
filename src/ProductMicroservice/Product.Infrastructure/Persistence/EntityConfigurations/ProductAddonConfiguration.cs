using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductAddonConfiguration : IEntityTypeConfiguration<ProductAddon>
    {
        public void Configure(EntityTypeBuilder<ProductAddon> builder)
        {
            // Table name
            builder.ToTable("product_addon");

            // Primary Key
            builder.HasKey(p => p.Id)
                   .HasName("pk_product_addon_id");

            // Properties with snake_case
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(300);
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
            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.DisplayOrder).HasColumnName("display_order").HasDefaultValue(0);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            // Foreign key
            builder.Property(p => p.ProductId).HasColumnName("product_id");
            builder.Property(p => p.ProductVariantId).HasColumnName("product_variant_id");

            builder.HasOne(p => p.Product)
                   .WithMany(p => p.ProductAddons)
                   .HasForeignKey(p => p.ProductId)
                   .HasConstraintName("fk_product_addon_product_id")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ProductVariantMaster)
                   .WithMany(p => p.ProductAddons)
                   .HasForeignKey(p => p.ProductVariantId)
                   .HasConstraintName("fk_product_addon_product_variant_id")
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
