using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakExpertiseConfiguration : IEntityTypeConfiguration<KathavachakExpertise>
    {
        public void Configure(EntityTypeBuilder<KathavachakExpertise> builder)
        {
            builder.ToTable("kathavachak_experties");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .HasColumnName("cat_id")
                .IsRequired();

            builder.Property(c => c.SubCategoryId)
               .HasColumnName("subcat_id")
               .IsRequired();

            builder.Property(c => c.ProficiencyLevel)
                .HasColumnName("proficiency_level")
                .IsRequired();

            builder.Property(c => c.YearsOfExperience)
                .HasColumnName("yrs_of_exp")
                .IsRequired();

            builder.Property(ae => ae.Description).HasColumnName("description");
            builder.Property(ae => ae.Price).HasColumnName("price");
            builder.Property(ae => ae.Duration).HasColumnName("duration");
            builder.Property(ae => ae.IsActive).HasColumnName("is_active");

            builder.Property(p => p.CategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("category_name_snap");

            builder.Property(p => p.SubCategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sub_cat_name_snap");

            builder.HasOne(c => c.Kathavachak)
                 .WithMany(k => k.Expertises)
                 .HasForeignKey(c => c.KathavachakId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pe => pe.KathavachakAttributeValues)
                   .WithOne(av => av.Expertise)
                   .HasForeignKey(av => av.ExpertiseId)
                   .HasConstraintName("fk_kathavachak_attribute_value_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
