using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerExpertiseEntityConfiguration : IEntityTypeConfiguration<AstrologerExpertise>
    {
        public void Configure(EntityTypeBuilder<AstrologerExpertise> builder)
        {
            builder.ToTable("astrologer_expertises");

            // Primary Key
            builder.HasKey(ae => ae.Id);
            builder.Property(ae => ae.Id).HasColumnName("id");

            // Foreign Keys
            builder.Property(ae => ae.AstrologerId).HasColumnName("astrologer_id");
            builder.Property(ae => ae.CategoryId).HasColumnName("category_id");
            builder.Property(ae => ae.SubCategoryId).HasColumnName("sub_category_id");

            // Expertise Info
            builder.Property(ae => ae.YearsOfExperience).HasColumnName("years_of_experience");
            builder.Property(ae => ae.ProficiencyLevel).HasColumnName("proficiency_level");

            // Package Info
            builder.Property(ae => ae.Name).HasColumnName("name").HasMaxLength(100);
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
            builder.HasOne(ae => ae.Astrologer)
                   .WithMany(a => a.AstrologerExpertises)
                   .HasForeignKey(ae => ae.AstrologerId)
                   .HasConstraintName("fk_astrologer_expertise_astrologer_id");

            builder.HasMany(pe => pe.ConsultationModes)
                   .WithOne(cm => cm.Expertise)
                   .HasForeignKey(cm => cm.ExpertiseId)
                   .HasConstraintName("fk_astrologer_consultation_mode_expertise_id");

            builder.HasMany(pe => pe.AstrologerAttributeValues)
                   .WithOne(av => av.AstrologerExpertise)
                   .HasForeignKey(av => av.ExpertiseId)
                   .HasConstraintName("fk_astrologer_attribute_value_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}