using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
       public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
       {
              public void Configure(EntityTypeBuilder<TimeSlot> builder)
              {
                     builder.ToTable("time_slots");

                     builder.HasKey(ts => ts.Id);

                     builder.Property(ts => ts.Id)
                            .HasColumnName("id")
                            .ValueGeneratedOnAdd();

                     builder.Property(ts => ts.AstrologerId)
                            .HasColumnName("astrologer_id")
                            .IsRequired();

                     builder.Property(ts => ts.StartUtc)
                            .HasColumnName("start_utc")
                            .IsRequired();

                     builder.Property(ts => ts.EndUtc)
                            .HasColumnName("end_utc")
                            .IsRequired();

                     builder.Property(ts => ts.IsBooked)
                            .HasColumnName("is_booked")
                            .IsRequired()
                            .HasDefaultValue(false);

                     builder.HasOne(ts => ts.Astrologer)
                            .WithMany(a => a.TimeSlots)
                            .HasForeignKey(ts => ts.AstrologerId)
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasIndex(ts => ts.AstrologerId)
                            .HasDatabaseName("ix_time_slots_astrologer_id");
              }
       }

}