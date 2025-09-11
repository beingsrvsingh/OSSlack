using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleExpertiseConfiguration : IEntityTypeConfiguration<TempleExpertise>
    {
        public void Configure(EntityTypeBuilder<TempleExpertise> builder)
        {
            builder.ToTable("temple_expertise");

            builder.HasKey(te => te.Id);

            builder.Property(te => te.Id)
                .HasColumnName("id");

            builder.Property(te => te.TempleId)
                .IsRequired()
                .HasColumnName("temple_id");

            builder.Property(te => te.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.Property(te => te.SubCategoryId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.Property(te => te.CategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("category_name_snapshot");

            builder.Property(te => te.SubCategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sub_category_name_snapshot");

            builder.Property(te => te.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(te => te.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            builder.Property(te => te.HasSchedule)
                .HasDefaultValue(false)
                .HasColumnName("has_schedule");

            builder.Property(te => te.Price)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("price");

            builder.Property(te => te.Duration)
                .HasColumnName("duration");

            builder.Property(te => te.AverageRating)
                .HasColumnType("decimal(3,2)")
                .HasDefaultValue(0)
                .HasColumnName("average_rating");

            builder.Property(te => te.TotalRatings)
                .HasDefaultValue(0)
                .HasColumnName("total_ratings");

            builder.Property(te => te.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(te => te.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(te => te.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("updated_at");

            // Relationships
            builder.HasOne(te => te.Temple)
                .WithMany(t => t.TempleExpertises)
                .HasForeignKey(te => te.TempleId)
                .HasConstraintName("fk_temple_expertise_temple_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(te => te.AttributeValues)
                .WithOne(av => av.TempleExpertise)
                .HasForeignKey(av => av.ExpertiseId) // Assumes such a FK exists
                .HasConstraintName("fk_attribute_value_expertise_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
