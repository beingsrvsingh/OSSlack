using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestConsultationModeConfiguration : IEntityTypeConfiguration<ConsultationMode>
    {
        public void Configure(EntityTypeBuilder<ConsultationMode> builder)
        {
            builder.ToTable("priest_consultation_mode");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.PriestId).HasColumnName("priest_id").IsRequired();
            builder.Property(c => c.ConsultationModeType).HasColumnName("mode").IsRequired();

            builder.HasOne(c => c.Priest)
                   .WithMany(p => p.ConsultationModes)
                   .HasForeignKey(c => c.PriestId);
        }
    }

}