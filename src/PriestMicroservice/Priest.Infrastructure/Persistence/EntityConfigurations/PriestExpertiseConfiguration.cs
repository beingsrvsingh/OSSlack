using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace PriestMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestExpertiseConfiguration : IEntityTypeConfiguration<PriestExpertise>
    {
        public void Configure(EntityTypeBuilder<PriestExpertise> builder)
        {
            builder.ToTable("priest_expertise");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.PriestId)
                .HasColumnName("priest_id");

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
            builder.HasOne(ae => ae.PriestMaster)
                   .WithMany(a => a.VariantMasters)
                   .HasForeignKey(ae => ae.PriestId)
                   .HasConstraintName("fk_priest_expertise_priest_id");

            builder.HasMany(pe => pe.ConsultationModes)
                   .WithOne(cm => cm.Expertise)
                   .HasForeignKey(cm => cm.ExpertiseId)
                   .HasConstraintName("fk_priest_consultation_mode_expertise_id");

            builder.HasMany(pe => pe.Attributes)
                   .WithOne(av => av.PriestExpertise)
                   .HasForeignKey(av => av.ExpertiseId)
                   .HasConstraintName("fk_priest_attribute_value_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Medias)
                .WithOne(vi => vi.PriestExpertise)
                .HasForeignKey(vi => vi.PriestExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Addons)
                .WithOne(a => a.PriestExpertise)
                .HasForeignKey(a => a.PriestExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}