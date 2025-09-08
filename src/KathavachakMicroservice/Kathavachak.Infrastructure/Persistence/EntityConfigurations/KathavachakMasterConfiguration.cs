using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{   
    public class KathavachakMasterConfiguration : IEntityTypeConfiguration<KathavachakMaster>
    {
        public void Configure(EntityTypeBuilder<KathavachakMaster> builder)
        {
            builder.ToTable("kathavachak_master");

            builder.HasKey(k => k.Id);

            builder.Property(k => k.Id)
                .HasColumnName("id");

            builder.Property(k => k.UserId)
                .HasColumnName("user_id")
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(k => k.Name)
                .HasColumnName("name")
                .HasMaxLength(200);

            builder.Property(k => k.ProfilePictureUrl)
                .HasColumnName("profile_picture_url")
                .HasMaxLength(500);

            builder.Property(k => k.AverageRating)
                .HasColumnName("average_rating")
                .HasColumnType("decimal(3,2)")
                .HasDefaultValue(0m);

            builder.Property(k => k.TotalRatings)
                .HasColumnName("total_ratings")
                .HasDefaultValue(0);

            builder.Property(k => k.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(k => k.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(k => k.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            // Navigation properties

            builder.HasMany(k => k.Expertises)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Languages)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Topics)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.SessionModes)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Schedules)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.TimeSlots)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Media)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
