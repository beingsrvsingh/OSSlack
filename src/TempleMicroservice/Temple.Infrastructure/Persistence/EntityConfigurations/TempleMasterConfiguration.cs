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

            // Relationships
            builder.HasMany(t => t.TempleExpertises)
                .WithOne(te => te.Temple)
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
        }

    }

}