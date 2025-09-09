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

            builder.Property(x => x.Mode)
                .HasColumnName("mode")
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(x => x.Expertise)
                .WithMany(e => e.ConsultationModes)
                .HasForeignKey(x => x.ExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ConsultationModeMaster)
                 .WithMany(x => x.ConsultationModes)
                .HasForeignKey(x => x.ConsultationModeMasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}