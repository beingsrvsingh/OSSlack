using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace PriestMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestExpertiseConfiguration : IEntityTypeConfiguration<PriestExpertise>
    {
        public void Configure(EntityTypeBuilder<PriestExpertise> builder)
        {
            builder.ToTable("priest_expertises");

            // Primary Key
            builder.HasKey(ae => ae.Id);
            builder.Property(ae => ae.Id).HasColumnName("id");

            // Foreign Keys
            builder.Property(ae => ae.PriestId).HasColumnName("priest_id");
            builder.Property(ae => ae.CategoryId).HasColumnName("category_id");
            builder.Property(ae => ae.SubCategoryId).HasColumnName("sub_category_id");

            // Expertise Info
            builder.Property(ae => ae.YearsOfExperience).HasColumnName("years_of_experience");
            builder.Property(ae => ae.ProficiencyLevel).HasColumnName("proficiency_level");

            // Package Info
            builder.Property(ae => ae.Name).HasColumnName("name");
            builder.Property(ae => ae.Description).HasColumnName("description");
            builder.Property(ae => ae.Price).HasColumnName("price");
            builder.Property(ae => ae.Duration).HasColumnName("duration");
            builder.Property(ae => ae.IsActive).HasColumnName("is_active");

            // Snapshots
            builder.Property(ae => ae.SubCategoryNameSnapshot)
                   .HasColumnName("sub_cat_name_snap")
                   .HasMaxLength(100);
            builder.Property(ae => ae.CategoryNameSnapshot)
                   .HasColumnName("category_name_snap")
                   .HasMaxLength(100);

            // Relationships
            builder.HasOne(pe => pe.Priest)
                    .WithMany(p => p.PriestExpertise)
                    .HasForeignKey(pe => pe.PriestId)
                    .HasConstraintName("fk_priest_expertise_priest_id")
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pe => pe.ConsultationModes)
                   .WithOne(cm => cm.Expertise)
                   .HasForeignKey(cm => cm.ExpertiseId)
                   .HasConstraintName("fk_priest_consultation_mode_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pe => pe.AttributeValues)
                   .WithOne(av => av.PriestExpertise)
                   .HasForeignKey(av => av.ExpertiseId)
                   .HasConstraintName("fk_priest_attribute_value_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}