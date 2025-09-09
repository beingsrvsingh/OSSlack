using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("schedule");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.PriestId).IsRequired().HasColumnName("priest_id");
            builder.Property(s => s.Date).IsRequired().HasColumnName("date");
            builder.Property(s => s.PriestId).HasColumnName("priest_id").IsRequired();
            builder.Property(s => s.DayOfWeek).HasColumnName("day_of_week").IsRequired();
            builder.Property(s => s.IsAvailable).HasColumnName("is_available").IsRequired();

            builder.HasOne(s => s.Priest)
               .WithMany(p => p.Schedules)
               .HasForeignKey(s => s.PriestId)
               .HasConstraintName("fk_schedules_priest_id")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }

}