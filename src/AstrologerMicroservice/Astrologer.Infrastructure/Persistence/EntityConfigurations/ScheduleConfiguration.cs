using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("schedules");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("id");

            builder.Property(s => s.AstrologerId)
                   .HasColumnName("astrologer_id")
                   .IsRequired();

            builder.Property(s => s.Day)
                   .HasColumnName("day_of_week")
                   .IsRequired();

            builder.Property(s => s.StartTime)
                   .HasColumnName("start_time")
                   .IsRequired();

            builder.Property(s => s.EndTime)
                   .HasColumnName("end_time")
                   .IsRequired();

            builder.Property(s => s.IsAvailable)
                   .HasColumnName("is_available")
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasIndex(s => new { s.AstrologerId, s.Day })
                   .HasDatabaseName("ix_schedules_astrologer_day")
                   .IsUnique();

            builder.HasOne(s => s.Astrologer)
                   .WithMany(a => a.Schedules)
                   .HasForeignKey(s => s.AstrologerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}