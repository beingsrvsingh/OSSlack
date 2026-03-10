using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestMasterConfiguration : IEntityTypeConfiguration<PriestMaster>
    {
        public void Configure(EntityTypeBuilder<PriestMaster> builder)
        {
            builder.ToTable("priest_master");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(p => p.IsActive)
                .HasColumnName("is_active");

            builder.Property(p => p.Rating)
                .HasColumnName("rating");

            builder.Property(p => p.Reviews)
                .HasColumnName("reviews");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at");

            // Relationships

            builder.HasMany(p => p.PriestExpertises)
                .WithOne(e => e.PriestMaster)
                .HasForeignKey(e => e.PriestId)
                .HasConstraintName("fk_priest_expertise_priest_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PriestLanguages)
                .WithOne(pl => pl.Priest)
                .HasForeignKey(pl => pl.PriestId)
                .HasConstraintName("fk_priest_language_priest_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Schedules)
                .WithOne(s => s.Priest)
                .HasForeignKey(s => s.PriestId)
                .HasConstraintName("fk_priest_schedule_priest_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PriestMedias)
                .WithOne(m => m.PriestMaster)
                .HasForeignKey(m => m.PriestId)
                .HasConstraintName("fk_priest_media_priest_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ScheduleExceptions)
                .WithOne(se => se.Priest)
                .HasForeignKey(se => se.PriestId)
                .HasConstraintName("fk_priest_schedule_exception_priest_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}