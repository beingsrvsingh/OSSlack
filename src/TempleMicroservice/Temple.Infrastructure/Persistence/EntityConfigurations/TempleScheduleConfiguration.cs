using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;


namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleScheduleConfiguration : IEntityTypeConfiguration<TempleSchedule>
    {
        public void Configure(EntityTypeBuilder<TempleSchedule> builder)
        {
            builder.ToTable("TempleSchedule");

            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.TempleMasterId)
                   .IsRequired();

            builder.Property(ts => ts.DayOfWeek)
                   .IsRequired();

            builder.Property(ts => ts.OpenTime)
                   .IsRequired();

            builder.Property(ts => ts.CloseTime)
                   .IsRequired();

            builder.HasOne(ts => ts.TempleMaster)
                   .WithMany(tm => tm.TempleSchedules)
                   .HasForeignKey(ts => ts.TempleMasterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }


}