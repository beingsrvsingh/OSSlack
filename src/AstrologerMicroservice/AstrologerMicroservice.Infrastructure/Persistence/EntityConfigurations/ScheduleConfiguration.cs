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
                   .HasColumnName("astrologer_id");

            builder.Property(s => s.Day)
                   .HasColumnName("day_of_week");

            builder.Property(s => s.StartTime)
                   .HasColumnName("start_time");

            builder.Property(s => s.EndTime)
                   .HasColumnName("end_time");

            builder.HasIndex(s => new { s.AstrologerId, s.Day })
                   .HasDatabaseName("ix_schedules_astrologer_day");

            builder.HasOne(s => s.Astrologer)
                   .WithMany(a => a.Schedules)
                   .HasForeignKey(s => s.AstrologerId);
        }
    }


}