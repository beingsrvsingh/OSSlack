using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.EntitiesConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.OrderHeaderId)
                .IsRequired()
                .HasColumnName("order_header_id");

            builder.Property(e => e.ProductVariantId)
                .IsRequired()
                .HasColumnName("product_variant_id");

            builder.Property(e => e.ProductType)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("product_type");

            builder.Property(e => e.ProductUrl)
                .HasMaxLength(300)
                .HasColumnName("product_url");

            builder.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("product_name");

            builder.Property(e => e.Sku)
                .HasMaxLength(100)
                .HasColumnName("sku");

            builder.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            builder.Property(e => e.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("unit_price");

            builder.Property(e => e.TaxAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("tax_amount");

            builder.Property(e => e.DiscountAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .HasColumnName("discount_amount");

            builder.Property(e => e.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("total_price");

            builder.Property(e => e.WeightValue)
                .HasColumnType("decimal(18,4)")
                .HasColumnName("weight_value");

            builder.Property(e => e.WeightUnit)
                .HasMaxLength(20)
                .HasColumnName("weight_unit");

            builder.Property(e => e.ProductOptions)
                .HasMaxLength(1000)
                .HasColumnName("product_options");

            builder.Property(e => e.FulfillmentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("fulfillment_status");

            builder.Property(e => e.ReturnStatus)
                .HasMaxLength(50)
                .HasDefaultValue("None")
                .HasColumnName("return_status");

            builder.Property(e => e.CustomerNotes)
                .HasMaxLength(1000)
                .HasColumnName("customer_notes");

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");

            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");

            // Relationships
            builder.HasOne(e => e.OrderHeader)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}