using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    internal class PoojaVariantMasterConfiguration : IEntityTypeConfiguration<PoojaVariantMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaVariantMaster> builder)
        {
            builder.ToTable("pooja_variant_master");

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

            builder.Property(v => v.DurationMinutes)
                .HasColumnName("duration_minute");

            builder.Property(v => v.BookingType)
                .HasColumnName("booking_type")
                .HasConversion<String>();

            builder.Property(v => v.IsDefault)
                .HasColumnName("is_default");

            builder.Property(v => v.PoojaId)
                .HasColumnName("pooja_master_id");

            builder.HasOne(v => v.PoojaMaster)
                .WithMany(p => p.VariantMasters)   // navigation property in ProductMaster
                .HasForeignKey(v => v.PoojaId) // matches the FK property
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships for VariantImages
            builder.HasMany(v => v.Medias)
                .WithOne(vi => vi.PoojaVariantMaster)
                .HasForeignKey(vi => vi.PoojaVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships for Attributes
            builder.HasMany(v => v.Attributes)
                .WithOne(a => a.PoojaVariantMaster)
                .HasForeignKey(a => a.PoojaVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Addons)
                .WithOne(a => a.PoojaVariantMaster)
                .HasForeignKey(a => a.PoojaVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
