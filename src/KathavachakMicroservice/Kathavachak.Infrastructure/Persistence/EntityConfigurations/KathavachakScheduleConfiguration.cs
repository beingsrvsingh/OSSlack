using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakScheduleConfiguration : IEntityTypeConfiguration<KathavachakSchedule>
    {
        public void Configure(EntityTypeBuilder<KathavachakSchedule> builder)
        {
            builder.ToTable("kathavachak_schedule");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id");

            builder.Property(s => s.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            builder.Property(s => s.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            builder.Property(s => s.EndDate)
                .HasColumnName("end_date")
                .IsRequired();

            builder.Property(s => s.IsAvailable)
                .HasColumnName("is_available")
                .HasDefaultValue(true);

            builder.Property(s => s.IsRecurring)
                .HasColumnName("is_recurring")
                .HasDefaultValue(true);

            builder.HasOne(s => s.Kathavachak)
                .WithMany(k => k.Schedules)
                .HasForeignKey(s => s.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}