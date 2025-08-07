using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.EntitiesConfigurations
{
    public class OrderItemCustomizationConfiguration : IEntityTypeConfiguration<OrderItemCustomization>
    {
        public void Configure(EntityTypeBuilder<OrderItemCustomization> builder)
        {
            builder.ToTable("order_item_customizations");

            builder.HasKey(oic => oic.Id)
                   .HasName("pk_order_item_customizations");

            builder.Property(oic => oic.Id)
                   .HasColumnName("id");

            builder.Property(oic => oic.OrderItemId)
                   .IsRequired()
                   .HasColumnName("order_item_id");

            builder.Property(oic => oic.OptionName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("option_name");

            builder.Property(oic => oic.OptionValue)
                   .IsRequired()
                   .HasMaxLength(500)
                   .HasColumnName("option_value");

            builder.Property(oic => oic.AdditionalNotes)
                   .HasMaxLength(1000)
                   .HasColumnName("additional_notes");

            builder.Property(oic => oic.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(oic => oic.UpdatedAt)
                   .HasColumnName("updated_at");

            builder.Property(oic => oic.CreatedBy)
                   .HasMaxLength(50)
                   .HasColumnName("created_by");

            builder.Property(oic => oic.UpdatedBy)
                   .HasMaxLength(50)
                   .HasColumnName("updated_by");

            // Foreign key relation to OrderItem
            builder.HasOne(oic => oic.OrderItem)
                   .WithMany(oi => oi.Customizations)
                   .HasForeignKey(oic => oic.OrderItemId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("fk_order_item_customizations_order_item");
        }
    }

}