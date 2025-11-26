using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleAddonConfiguration : IEntityTypeConfiguration<TempleAddon>
    {
        public void Configure(EntityTypeBuilder<TempleAddon> builder)
        {
            // Table name
            builder.ToTable("temple_addon");

            // Primary Key
            builder.HasKey(p => p.Id)
                   .HasName("pk_temple_addon_id");

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
            builder.Property(p => p.TempleId).HasColumnName("temple_id");
            builder.Property(p => p.TempleExpertiseId).HasColumnName("temple_expertise_id");

            builder.HasOne(p => p.TempleMaster)
                   .WithMany(p => p.TempleAddons)
                   .HasForeignKey(p => p.TempleId)
                   .HasConstraintName("fk_temple_addon_temple_id")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.TempleExpertise)
                   .WithMany(p => p.TempleAddons)
                   .HasForeignKey(p => p.TempleExpertiseId)
                   .HasConstraintName("fk_temple_addon_temple_variant_id")
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
