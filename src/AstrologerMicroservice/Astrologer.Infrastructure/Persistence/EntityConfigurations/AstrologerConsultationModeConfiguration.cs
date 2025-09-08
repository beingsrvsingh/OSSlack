using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerConsultationModeConfiguration : IEntityTypeConfiguration<AstrologerConsultationMode>
    {
        public void Configure(EntityTypeBuilder<AstrologerConsultationMode> builder)
        {
            builder.ToTable("astrologer_consultation_mode");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.ExpertiseId)
                .HasColumnName("expertise_id")
                .IsRequired();

            builder.Property(x => x.ConsultationModeMasterId)
                .HasColumnName("consultation_mode_master_id")
                .IsRequired();

            builder.Property(x => x.ConsultationMode)
                .HasColumnName("consultation_mode")
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(x => x.Expertise)
                .WithMany(e => e.ConsultationModes)
                .HasForeignKey(x => x.ExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ConsultationModeMaster)
                 .WithMany(x => x.AstrologerConsultationModes)
                .HasForeignKey(x => x.ConsultationModeMasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }


}