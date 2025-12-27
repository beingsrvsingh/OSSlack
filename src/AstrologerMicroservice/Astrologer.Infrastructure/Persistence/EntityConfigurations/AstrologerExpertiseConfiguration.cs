using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerExpertiseConfiguration : IEntityTypeConfiguration<AstrologerExpertise>
    {
        public void Configure(EntityTypeBuilder<AstrologerExpertise> builder)
        {
            builder.ToTable("astrologer_expertise");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.AstrologerId)
                .HasColumnName("astrologer_id");

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

            // Relationships
            builder.HasOne(ae => ae.Astrologer)
                   .WithMany(a => a.VariantMasters)
                   .HasForeignKey(ae => ae.AstrologerId)
                   .HasConstraintName("fk_astrologer_expertise_astrologer_id");

            builder.HasMany(pe => pe.ConsultationModes)
                   .WithOne(cm => cm.Expertise)
                   .HasForeignKey(cm => cm.ExpertiseId)
                   .HasConstraintName("fk_astrologer_consultation_mode_expertise_id");

            builder.HasMany(pe => pe.Attributes)
                   .WithOne(av => av.AstrologerExpertise)
                   .HasForeignKey(av => av.ExpertiseId)
                   .HasConstraintName("fk_astrologer_attribute_value_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Media)
                .WithOne(vi => vi.AstrologerExpertise)
                .HasForeignKey(vi => vi.AstrolgerExpertiesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Addons)
                .WithOne(a => a.AstrologerExpertise)
                .HasForeignKey(a => a.AstrologerExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}