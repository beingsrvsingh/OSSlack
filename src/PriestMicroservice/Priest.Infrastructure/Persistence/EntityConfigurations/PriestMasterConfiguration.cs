using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestMasterConfiguration : IEntityTypeConfiguration<PriestMaster>
    {
        public void Configure(EntityTypeBuilder<PriestMaster> builder)
        {
            builder.ToTable("priest_master");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                .HasColumnName("user_id")
                .HasMaxLength(36)
                .IsRequired();

            builder.Property(p => p.DisplayName)
                .HasColumnName("display_name")
                .HasMaxLength(200);

            builder.Property(p => p.ProfilePictureUrl)
                .HasColumnName("profile_picture_url")
                .HasMaxLength(500);

            builder.Property(p => p.AverageRating)
                .HasColumnName("average_rating")
                .HasColumnType("decimal(3,2)")
                .HasDefaultValue(0m);

            builder.Property(p => p.TotalRatings)
                .HasColumnName("total_ratings")
                .HasDefaultValue(0);

            builder.Property(p => p.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasMany(p => p.PriestLanguages).WithOne(l => l.Priest).HasForeignKey(l => l.PriestId).OnDelete(DeleteBehavior.Cascade);;
            builder.HasMany(p => p.PriestExpertise).WithOne(e => e.Priest).HasForeignKey(e => e.PriestId).OnDelete(DeleteBehavior.Cascade);;
            builder.HasMany(p => p.ConsultationModes).WithOne(c => c.Priest).HasForeignKey(c => c.PriestId).OnDelete(DeleteBehavior.Cascade);;

            builder.HasMany(p => p.Schedules)
                .WithOne(s => s.Priest)
                .HasForeignKey(s => s.PriestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.RitualServicePackages)
                .WithOne(sp => sp.Priest)
                .HasForeignKey(sp => sp.PriestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}