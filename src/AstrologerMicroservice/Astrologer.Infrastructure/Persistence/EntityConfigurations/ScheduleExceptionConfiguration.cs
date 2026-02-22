using Astrologer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrologer.Infrastructure.Persistence.EntityConfigurations
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
            builder.Property(se => se.AstrologerId)
                    .HasColumnName("astrologer_id")
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
            builder.HasOne(se => se.Astrologer)
                   .WithMany(a => a.ScheduleExceptions) // assuming you don't have a ScheduleExceptions collection on Astrologer
                   .HasForeignKey(se => se.AstrologerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional: Index on AstrologerId + Date for faster lookup
            builder.HasIndex(se => new { se.AstrologerId, se.Date }).IsUnique(false);
        }
    }
}
