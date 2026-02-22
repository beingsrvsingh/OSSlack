using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;


namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("schedules");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("id");

            builder.Property(s => s.TempleMasterId)
                   .HasColumnName("temple_id")
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

            builder.HasOne(s => s.TempleMaster)
                 .WithMany(k => k.Schedules)
                 .HasForeignKey(s => s.TempleMasterId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }

}