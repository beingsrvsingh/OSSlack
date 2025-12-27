using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleExpertiseConfiguration : IEntityTypeConfiguration<TempleExpertise>
    {
        public void Configure(EntityTypeBuilder<TempleExpertise> builder)
        {
            builder.ToTable("temple_expertise");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.TempleId)
                .HasColumnName("temple_id");

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

            builder.Property(v => v.AvailableSlots)
                .HasColumnName("available_slots");

            builder.Property(v => v.IsDefault)
                .HasColumnName("is_default");

            // Relationships
            builder.HasOne(te => te.TempleMaster)
                .WithMany(t => t.VariantMasters)
                .HasForeignKey(te => te.TempleId)
                .HasConstraintName("fk_temple_expertise_temple_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(te => te.Attributes)
                .WithOne(av => av.TempleExpertise)
                .HasForeignKey(av => av.ExpertiseId)
                .HasConstraintName("fk_attribute_value_expertise_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Addons)
                .WithOne(a => a.TempleExpertise)
                .HasForeignKey(a => a.TempleExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Media)
                .WithOne(vi => vi.TempleExpertise)
                .HasForeignKey(vi => vi.TempleExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
