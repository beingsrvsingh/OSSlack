using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class ScheduleExceptionConfiguration : IEntityTypeConfiguration<ScheduleException>
    {
        public void Configure(EntityTypeBuilder<ScheduleException> builder)
        {
            // Table name
            builder.ToTable("schedule_exceptions");

            // Primary key
            builder.HasKey(se => se.Id);

            // Columns
            builder.Property(se => se.KathavachakId)
                    .HasColumnName("kathavachak_id")
                   .IsRequired();

            builder.Property(se => se.Date)
                   .IsRequired()
                   .HasColumnName("date");

            builder.Property(se => se.StartTime)
                .HasColumnName("start_time");

            builder.Property(se => se.EndTime)
                    .HasColumnName("end_time");

            builder.Property(se => se.IsBlocked)
                   .IsRequired()
                   .HasColumnName("is_blocked")
                   .HasDefaultValue(true);

            // Relationships
            builder.HasOne(se => se.Kathavachak)
                   .WithMany(a => a.ScheduleExceptions) // assuming you don't have a ScheduleExceptions collection on Kathavachak
                   .HasForeignKey(se => se.KathavachakId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional: Index on KathavachakId + Date for faster lookup
            builder.HasIndex(se => new { se.KathavachakId, se.Date }).IsUnique(false);
        }
    }
}
