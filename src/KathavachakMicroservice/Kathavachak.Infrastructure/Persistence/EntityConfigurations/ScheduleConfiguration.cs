using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("schedules");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("id");

            builder.Property(s => s.KathavachakId)
                   .HasColumnName("kathavachak_id")
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

            builder.HasOne(s => s.Kathavachak)
                .WithMany(k => k.Schedules)
                .HasForeignKey(s => s.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}