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

            builder.Property(a => a.UserId)
                   .IsRequired()
                   .HasMaxLength(36);


            builder.Property(a => a.DisplayName)
                   .HasMaxLength(200);

            builder.Property(a => a.ProfilePictureUrl)
                   .HasMaxLength(500);

            builder.Property(a => a.AverageRating)
                   .HasColumnType("decimal(3,2)")
                   .HasDefaultValue(0m);

            builder.Property(a => a.TotalRatings)
                   .HasDefaultValue(0);

            builder.Property(a => a.IsActive)
                   .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(a => a.UpdatedAt)
                   .IsRequired(false);

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