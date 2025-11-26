using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaAddonConfiguration : IEntityTypeConfiguration<PoojaAddon>
    {
        public void Configure(EntityTypeBuilder<PoojaAddon> builder)
        {
            // Table name
            builder.ToTable("pooja_addon");

            // Primary Key
            builder.HasKey(p => p.Id)
                   .HasName("pk_pooja_addon_id");

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
            builder.Property(p => p.PoojaId).HasColumnName("pooja_id");
            builder.Property(p => p.PoojaVariantId).HasColumnName("pooja_variant_id");

            builder.HasOne(p => p.PoojaMaster)
                   .WithMany(p => p.PoojaAddons)
                   .HasForeignKey(p => p.PoojaId)
                   .HasConstraintName("fk_pooja_addon_pooja_id")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PoojaVariantMaster)
                   .WithMany(p => p.PoojaAddons)
                   .HasForeignKey(p => p.PoojaVariantId)
                   .HasConstraintName("fk_pooja_addon_pooja_variant_id")
                   .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
