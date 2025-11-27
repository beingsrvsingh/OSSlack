using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakExpertiseConfiguration : IEntityTypeConfiguration<KathavachakExpertise>
    {
        public void Configure(EntityTypeBuilder<KathavachakExpertise> builder)
        {
            builder.ToTable("kathavachak_expertise");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.KathavachakId)
                .HasColumnName("kathavachak_id");

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

            builder.HasOne(c => c.KathavachakMaster)
                 .WithMany(k => k.KathavachakExpertises)
                 .HasForeignKey(c => c.KathavachakId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pe => pe.KathavachakAttributeValues)
                   .WithOne(av => av.KathavachakExpertise)
                   .HasForeignKey(av => av.ExpertiseId)
                   .HasConstraintName("fk_kathavachak_attribute_value_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.KathavachakExpertiseMedia)
                .WithOne(vi => vi.KathavachakExpertise)
                .HasForeignKey(vi => vi.KathavachakExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.KathavachakAddons)
                .WithOne(a => a.KathavachakExpertise)
                .HasForeignKey(a => a.KathavachakExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
