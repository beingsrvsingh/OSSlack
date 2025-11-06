using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleMasterConfiguration : IEntityTypeConfiguration<TempleMaster>
    {
        public void Configure(EntityTypeBuilder<TempleMaster> builder)
        {
            builder.ToTable("temple_master");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.LocationId)
                .IsRequired()
                .HasColumnName("location_id");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.Property(t => t.ThumbnailUrl)
                .HasMaxLength(300)
                .HasColumnName("thumbnail_url");

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(t => t.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("updated_at");

            builder.Property(p => p.Rating)
                .HasColumnName("rating_snap");

            builder.Property(p => p.Reviews)
                .HasColumnName("reviews_snap");

            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.Property(p => p.SubCategoryId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.Property(p => p.CategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("category_name_snapshot");

            builder.Property(p => p.SubCategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sub_category_name_snapshot");

            builder.Property(p => p.IsTrending)
                .HasColumnName("is_trending");

            builder.Property(p => p.IsFeatured)
                .HasColumnName("is_featured");

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");

            // Relationships
            builder.HasMany(t => t.TempleExpertises)
                .WithOne(te => te.TempleMaster)
                .HasForeignKey(te => te.TempleId)
                .HasConstraintName("fk_temple_expertises_temple_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.TempleSchedules)
                .WithOne(ts => ts.TempleMaster)
                .HasForeignKey(ts => ts.TempleMasterId)
                .HasConstraintName("fk_temple_schedules_temple_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.TempleExceptions)
                .WithOne(te => te.TempleMaster)
                .HasForeignKey(te => te.TempleMasterId)
                .HasConstraintName("fk_temple_exceptions_temple_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.TempleImages)
                .WithOne(img => img.TempleMaster)
                .HasForeignKey(img => img.TempleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.TempleAddons)
                .WithOne(img => img.TempleMaster)
                .HasForeignKey(img => img.TempleId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}