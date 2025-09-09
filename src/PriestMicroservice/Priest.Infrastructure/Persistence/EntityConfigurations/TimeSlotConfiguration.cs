using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.ToTable("time_slot");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.ScheduleId).HasColumnName("schedule_id").IsRequired();
            builder.Property(t => t.StartTime).HasColumnName("start_time").IsRequired();
            builder.Property(t => t.EndTime).HasColumnName("end_time").IsRequired();
            builder.Property(t => t.IsBooked).HasColumnName("is_booked").HasDefaultValue(false);

            builder.HasOne(ts => ts.Schedule)
               .WithMany(s => s.TimeSlots)
               .HasForeignKey(ts => ts.ScheduleId)
               .HasConstraintName("fk_time_slots_schedule_id")
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ts => ts.ScheduleId)
                   .HasDatabaseName("ix_time_slots_schedule_id");
        }
    }

}