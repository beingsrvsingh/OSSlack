using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakTimeSlotConfiguration : IEntityTypeConfiguration<KathavachakTimeSlot>
    {
        public void Configure(EntityTypeBuilder<KathavachakTimeSlot> builder)
        {
            builder.ToTable("kathavachak_time_slot");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.ScheduleId)
                .HasColumnName("schedule_id")
                .IsRequired();

            builder.Property(t => t.StartTime)
                .HasColumnName("start_time")
                .IsRequired();

            builder.Property(t => t.EndTime)
                .HasColumnName("end_time")
                .IsRequired();

            builder.Property(t => t.DayOfWeek)
               .HasColumnName("day_of_week")
               .IsRequired();

            builder.Property(t => t.IsBooked)
                .HasColumnName("is_booked")
                .HasDefaultValue(false);

            builder.HasOne(t => t.Schedule)
                .WithMany(k => k.TimeSlots)
                .HasForeignKey(t => t.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ts => ts.ScheduleId)
                   .HasDatabaseName("ix_time_slots_schedule_id");
        }
    }
}
