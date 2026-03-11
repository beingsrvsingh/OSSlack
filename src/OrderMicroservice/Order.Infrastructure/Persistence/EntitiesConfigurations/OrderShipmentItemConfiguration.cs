using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.EntitiesConfigurations
{
    public class OrderShipmentItemConfiguration : IEntityTypeConfiguration<OrderShipmentItem>
    {
        public void Configure(EntityTypeBuilder<OrderShipmentItem> builder)
        {
            builder.ToTable("order_shipment_item");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnName("id");

            builder.Property(si => si.ShipmentId)
                .IsRequired()
                .HasColumnName("shipment_id");

            builder.Property(si => si.OrderItemId)
                .IsRequired()
                .HasColumnName("order_item_id");

            builder.Property(si => si.QuantityShipped)
                .IsRequired()
                .HasColumnName("quantity_shipped");

            builder.Property(si => si.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(si => si.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            // Relationships
            builder.HasOne(si => si.Shipment)
                .WithMany(s => s.ShipmentItems)
                .HasForeignKey(si => si.ShipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(si => si.OrderItem)
                .WithMany(oi => oi.ShipmentItems)
                .HasForeignKey(si => si.OrderItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(si => si.ShipmentId);
            builder.HasIndex(si => si.OrderItemId);
        }

    }
}
