using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.EntitiesConfigurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("order_header");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");

            builder.Property(o => o.OrderNumber)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("order_number");

            builder.Property(o => o.CustomerId)
                .IsRequired()
                .HasColumnName("customer_id");

            builder.Property(o => o.Status)
                .IsRequired()
                .HasColumnName("status");

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnName("order_date");

            builder.Property(o => o.ShippedDate)
                .HasColumnName("shipped_date");

            builder.Property(o => o.DeliveredDate)
                .HasColumnName("delivered_date");

            builder.Property(o => o.EstimatedDeliveryDate)
                .HasColumnName("estimated_delivery_date");

            builder.Property(o => o.ShippingMethod)
                .HasMaxLength(100)
                .HasColumnName("shipping_method");

            builder.Property(o => o.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("total_amount");

            builder.Property(o => o.TaxAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("tax_amount");

            builder.Property(o => o.ShippingFee)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("shipping_fee");

            builder.Property(o => o.PlatformFee)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("platform_fee");

            builder.Property(o => o.SurgeFee)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("surge_fee");

            builder.Property(o => o.DiscountAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("discount_amount");

            builder.Property(o => o.CurrencyCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasDefaultValue("USD")
                .HasColumnName("currency_code");

            builder.Property(o => o.IsGift)
                .IsRequired()
                .HasColumnName("is_gift");

            builder.Property(o => o.GiftMessage)
                .HasMaxLength(500)
                .HasColumnName("gift_message");

            builder.Property(o => o.CustomerNotes)
                .HasMaxLength(1000)
                .HasColumnName("customer_notes");

            builder.Property(o => o.AdminNotes)
                .HasMaxLength(1000)
                .HasColumnName("admin_notes");

            builder.Property(o => o.RefundStatus)
                .HasMaxLength(50)
                .HasColumnName("refund_status");

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(o => o.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(o => o.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");

            builder.Property(o => o.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");

            // Relations
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.OrderHeader)
                .HasForeignKey(oi => oi.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.ShippingAddresses)
                .WithOne(sa => sa.OrderHeader)
                .HasForeignKey(sa => sa.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}