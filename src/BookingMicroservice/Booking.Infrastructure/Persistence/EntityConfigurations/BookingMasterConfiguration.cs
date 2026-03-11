using BookingMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class BookingMasterConfiguration : IEntityTypeConfiguration<BookingMaster>
    {
        public void Configure(EntityTypeBuilder<BookingMaster> builder)
        {
            // Table name in snake_case
            builder.ToTable("bookings");

            // Primary Key
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Id)
                   .HasColumnName("id");

            builder.Property(b => b.BookingReferenceNumber)
                   .HasColumnName("booking_ref_num");

            builder.Property(b => b.EntityType)
                   .HasColumnName("entity_type")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.EntityId)
                   .HasColumnName("entity_id")
                   .IsRequired();

            builder.Property(b => b.UserId)
                   .HasColumnName("user_id");

            builder.Property(b => b.ProductName)
                   .HasColumnName("name")
                   .IsRequired();

            builder.Property(b => b.Date)
                   .HasColumnName("date")
                   .IsRequired();

            builder.Property(b => b.StartTime)
                   .HasColumnName("start_time")
                   .IsRequired();

            builder.Property(b => b.EndTime)
                   .HasColumnName("end_time")
                   .IsRequired();

            builder.Property(b => b.Source)
                   .HasColumnName("source")
                   .HasMaxLength(50)
                   .HasDefaultValue("web");

            builder.Property(ci => ci.BookingOptionsJson)
                .HasColumnType("json")
                .HasColumnName("booking_options");

            builder.Property(b => b.Status)
                   .HasColumnName("status")
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(BookingStatus.Pending);

            builder.Property(b => b.Notes)
                   .HasColumnName("notes")
                   .HasMaxLength(500);

            builder.Property(b => b.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(b => b.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                   .ValueGeneratedOnAddOrUpdate();

            // Unique index to prevent double booking for same entity + time
            builder.HasIndex(b => new { b.EntityType, b.EntityId, b.Date, b.StartTime, b.EndTime })
                   .IsUnique()
                   .HasDatabaseName("ux_booking_entity_time");
        }
    }
}
