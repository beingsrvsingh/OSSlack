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

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.PriestId)
                .HasColumnName("priest_id");

            builder.Property(e => e.CategoryId)
                .HasColumnName("category_id");

            builder.Property(e => e.SubCategoryId)
                .HasColumnName("sub_category_id");

            builder.Property(e => e.DurationMinutes)
                .HasColumnName("duration_minutes");

            builder.Property(e => e.IsDefault)
                .HasColumnName("is_default");

            // Relationships

            builder.HasOne(e => e.PriestMaster)
                .WithMany(p => p.PriestExpertises)
                .HasForeignKey(e => e.PriestId)
                .HasConstraintName("fk_priest_expertise_priest_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ConsultationModes)
                .WithOne(cm => cm.Expertise)
                .HasForeignKey(cm => cm.ExpertiseId)
                .HasConstraintName("fk_priest_consultation_mode_expertise_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}