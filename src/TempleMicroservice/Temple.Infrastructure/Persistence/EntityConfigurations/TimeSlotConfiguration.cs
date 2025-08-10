using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
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

                     builder.Property(ts => ts.TempleId)
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

                     builder.HasOne(ts => ts.TempleMaster)
                            .WithMany(a => a.TimeSlots)
                            .HasForeignKey(ts => ts.TempleId)
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasIndex(ts => ts.TempleId)
                            .HasDatabaseName("ix_time_slots_astrologer_id");
              }
       }

}