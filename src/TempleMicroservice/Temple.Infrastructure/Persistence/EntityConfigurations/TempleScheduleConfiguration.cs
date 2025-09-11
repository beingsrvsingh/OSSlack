using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;


namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleScheduleConfiguration : IEntityTypeConfiguration<TempleSchedule>
    {
        public void Configure(EntityTypeBuilder<TempleSchedule> builder)
        {
            builder.ToTable("temple_schedule");

            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Id)
                .HasColumnName("id");

            builder.Property(ts => ts.TempleMasterId)
                .IsRequired()
                .HasColumnName("temple_id");

            builder.Property(ts => ts.DayOfWeek)
                .IsRequired()
                .HasColumnName("day_of_week")
                .HasConversion<int>(); // store enum as int

            builder.Property(ts => ts.OpenTime)
                .IsRequired()
                .HasColumnName("open_time");

            builder.Property(ts => ts.CloseTime)
                .IsRequired()
                .HasColumnName("close_time");

            builder.Property(ts => ts.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(ts => ts.Reason)
                .HasMaxLength(100)
                .HasColumnName("reason");

            builder.HasOne(s => s.TempleMaster)
                 .WithMany(k => k.TempleSchedules)
                 .HasForeignKey(s => s.TempleMasterId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }

}