using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestConsultationModeConfiguration : IEntityTypeConfiguration<ConsultationMode>
    {
        public void Configure(EntityTypeBuilder<ConsultationMode> builder)
        {
            builder.ToTable("consultation_mode");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.ExpertiseId)
                .HasColumnName("expertise_id")
                .IsRequired();

            builder.Property(x => x.ConsultationModeMasterId)
                .HasColumnName("consultation_mode_id")
                .IsRequired();

            builder.Property(x => x.StockQuantity)
                .HasColumnName("stock_quantity");

            builder.Property(x => x.IsDefault)
                .HasColumnName("is_default");

            // Price (Owned Value Object)
            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18,2)");

                price.Property(p => p.Mrp)
                    .HasColumnName("mrp")
                    .HasColumnType("decimal(18,2)");

                price.Property(p => p.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3);

                price.Property(p => p.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(18,2)");

                price.Property(p => p.Tax)
                    .HasColumnName("tax")
                    .HasColumnType("decimal(18,2)");

                price.Property(p => p.EffectiveFrom)
                    .HasColumnName("price_effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                price.Property(p => p.EffectiveTo)
                    .HasColumnName("price_effective_to")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            // Relationships

            builder.HasOne(x => x.Expertise)
                .WithMany(e => e.ConsultationModes)
                .HasForeignKey(x => x.ExpertiseId)
                .HasConstraintName("fk_priest_consultation_mode_expertise_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ConsultationModeMaster)
                 .WithMany(x => x.ConsultationModes)
                 .HasForeignKey(x => x.ConsultationModeMasterId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }

}