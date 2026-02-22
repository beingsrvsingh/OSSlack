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
            builder.ToTable("booking_master");

            // Primary Key
            builder.HasKey(b => b.Id);

            // Properties with snake_case columns
            builder.Property(b => b.Id)
                   .HasColumnName("id");

            builder.Property(b => b.EntityType)
                   .HasColumnName("entity_type")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.EntityId)
                   .HasColumnName("entity_id")
                   .IsRequired();

            builder.Property(b => b.UserId)
                   .HasColumnName("user_id");

            builder.Property(b => b.PoojaType)
                   .HasColumnName("pooja_type")
                   .IsRequired()
                   .HasMaxLength(100);

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

            builder.Property(b => b.Status)
                   .HasColumnName("status")
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(BookingStatus.Pending);

            builder.Property(b => b.PaymentStatus)
                   .HasColumnName("payment_status")
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(PaymentStatus.Pending);

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
