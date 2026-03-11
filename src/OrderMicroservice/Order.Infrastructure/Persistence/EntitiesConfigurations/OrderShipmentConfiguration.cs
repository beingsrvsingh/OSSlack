using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence.EntitiesConfigurations
{
    public class OrderShipmentConfiguration : IEntityTypeConfiguration<OrderShipment>
    {
        public void Configure(EntityTypeBuilder<OrderShipment> builder)
        {
            builder.ToTable("order_shipment");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.OrderHeaderId)
                .IsRequired()
                .HasColumnName("order_header_id");

            builder.Property(s => s.ShippingMethod)
                .HasMaxLength(50)
                .HasColumnName("shipping_method");

            builder.Property(s => s.CarrierName)
                .HasMaxLength(50)
                .HasColumnName("carrier_name");

            builder.Property(s => s.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");

            builder.Property(s => s.ShippedDate)
                .HasColumnName("shipped_date");

            builder.Property(s => s.EstimatedDeliveryDate)
                .HasColumnName("estimated_delivery_date");

            builder.Property(s => s.DeliveredDate)
                .HasColumnName("delivered_date");

            builder.Property(s => s.ShipmentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("shipment_status");

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(s => s.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            // Relationships
            builder.HasOne(s => s.OrderHeader)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.ShipmentItems)
                .WithOne(si => si.Shipment)
                .HasForeignKey(si => si.ShipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(s => s.OrderHeaderId);
            builder.HasIndex(s => s.TrackingNumber);
        }

    }
}
