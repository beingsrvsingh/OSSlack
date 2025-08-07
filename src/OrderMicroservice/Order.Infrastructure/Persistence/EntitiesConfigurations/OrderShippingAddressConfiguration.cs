using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.EntitiesConfigurations
{
    public class OrderShippingAddressConfiguration : IEntityTypeConfiguration<OrderShippingAddress>
    {
        public void Configure(EntityTypeBuilder<OrderShippingAddress> builder)
        {
            builder.ToTable("order_shipping_address");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired()
                .HasColumnName("order_header_id");

            builder.Property(x => x.RecipientName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("recipient_name");

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("mobile_number");

            builder.Property(x => x.AddressLine1)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("address_line_1");

            builder.Property(x => x.AddressLine2)
                .HasMaxLength(250)
                .HasColumnName("address_line_2");

            builder.Property(x => x.City)
                .HasMaxLength(100)
                .HasColumnName("city");

            builder.Property(x => x.State)
                .HasMaxLength(100)
                .HasColumnName("state");

            builder.Property(x => x.Country)
                .HasMaxLength(100)
                .HasColumnName("country");

            builder.Property(x => x.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
        }
    }

}