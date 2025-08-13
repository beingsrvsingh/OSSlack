using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.ToTable("time_slot");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Id).HasColumnName("priest_id").IsRequired();
            builder.Property(t => t.StartTime).HasColumnName("start_time").IsRequired();
            builder.Property(t => t.EndTime).HasColumnName("end_time").IsRequired();
            builder.Property(t => t.IsBooked).HasColumnName("is_booked").HasDefaultValue(false);

            builder.HasOne(t => t.Schedule)
                   .WithMany(p => p.TimeSlots)
                   .HasForeignKey(t => t.Id);
        }
    }

}