using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace PriestMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ConsultationModeMasterConfiguration : IEntityTypeConfiguration<ConsultationModeMaster>
    {
        public void Configure(EntityTypeBuilder<ConsultationModeMaster> builder)
        {
            builder.ToTable("consultation_mode_master");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                   .IsRequired()
                   .HasColumnName("id");

            builder.Property(l => l.Mode)
                   .IsRequired()
                   .HasColumnName("mode")
                   .HasMaxLength(100);

            builder.Property(l => l.DisplayOrder)
                   .HasColumnName("display_order");

            builder.HasMany(c => c.ConsultationModes)
                .WithOne(acm => acm.ConsultationModeMaster)
                .HasForeignKey(acm => acm.ConsultationModeMasterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
