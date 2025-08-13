using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("schedule");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.PriestId).HasColumnName("priest_id").IsRequired();
            builder.Property(s => s.DayOfWeek).HasColumnName("day_of_week").IsRequired();

            builder.HasOne(s => s.Priest)
                   .WithMany(p => p.Schedules)
                   .HasForeignKey(s => s.PriestId);
        }
    }

}