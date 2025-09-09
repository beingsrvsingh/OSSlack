using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerEntityConfiguration : IEntityTypeConfiguration<AstrologerEntity>
    {
        public void Configure(EntityTypeBuilder<AstrologerEntity> builder)
        {
            builder.ToTable("astrologers");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasColumnName("id");

            builder.Property(a => a.UserId)
                   .IsRequired()
                   .HasMaxLength(36)
                   .HasColumnName("user_id");

            builder.Property(a => a.Name)
                   .HasMaxLength(200)
                   .HasColumnName("name");

            builder.Property(a => a.ThumbnailUrl)
                   .HasMaxLength(500)
                   .HasColumnName("thumbnail_url");

            builder.Property(a => a.AverageRating)
                   .HasColumnType("decimal(3,2)")
                   .HasDefaultValue(0m)
                   .HasColumnName("average_rating");

            builder.Property(a => a.TotalRatings)
                   .HasDefaultValue(0)
                   .HasColumnName("total_ratings");

            builder.Property(a => a.IsActive)
                   .HasDefaultValue(true)
                   .HasColumnName("is_active");

            builder.Property(a => a.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                   .HasColumnName("created_at");

            builder.Property(a => a.UpdatedAt)
                   .IsRequired(false)
                   .HasColumnName("updated_at");

            // Relationships

            builder.HasMany(a => a.AstrologerLanguages)
                   .WithOne(al => al.Astrologer)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_language_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AstrologerExpertises)
                   .WithOne(al => al.Astrologer)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_expertise_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Schedules)
                   .WithOne(al => al.Astrologer)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_schedules_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.TimeSlots)
                   .WithOne(ae => ae.Astrologer)
                   .HasForeignKey(ae => ae.AstrologerId)
                   .HasConstraintName("fk_astrologer_timeslots_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}