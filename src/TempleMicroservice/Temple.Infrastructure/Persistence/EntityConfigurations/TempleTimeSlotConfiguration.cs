using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleTimeSlotConfiguration : IEntityTypeConfiguration<TempleTimeSlot>
    {
        public void Configure(EntityTypeBuilder<TempleTimeSlot> builder)
        {
            builder.ToTable("temple_time_slot");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.ScheduleId)
                .IsRequired()
                .HasColumnName("schedule_id");

            builder.Property(t => t.SlotStartTime)
                .IsRequired()
                .HasColumnName("slot_start_time");

            builder.Property(t => t.SlotEndTime)
                .IsRequired()
                .HasColumnName("slot_end_time");

            builder.Property(t => t.MaxCapacity)
                .HasDefaultValue(0)
                .HasColumnName("max_capacity");

            builder.Property(t => t.BookedCount)
                .HasDefaultValue(0)
                .HasColumnName("booked_count");

            builder.Property(t => t.Label)
                .HasMaxLength(100)
                .HasColumnName("label");

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            // Navigation
            builder.HasOne(t => t.TempleSchedule)
                .WithMany(t => t.TimeSlots)
                .HasForeignKey(t => t.ScheduleId)
                .HasConstraintName("fk_schedule_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
