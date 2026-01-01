using CartMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class CartConfiguration : IEntityTypeConfiguration<CartMicroservice.Domain.Entities.Cart>
    {
        public void Configure(EntityTypeBuilder<CartMicroservice.Domain.Entities.Cart> builder)
        {
            builder.ToTable("cart");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("user_id");

            builder.Property(c => c.RegionCode)
                .HasMaxLength(50)
                .HasColumnName("region_code");

            builder.Property(c => c.CurrencyCode)
                .HasMaxLength(10)
                .HasDefaultValue("INR")
                .HasColumnName("currency_code");

            builder.Property(c => c.TenantId)
                .HasMaxLength(50)
                .HasColumnName("tenant_id");

            builder.Property(c => c.Subtotal)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("subtotal");

            builder.Property(c => c.TotalDiscount)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("total_discount");

            builder.Property(c => c.TotalTax)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("total_tax");

            builder.Property(c => c.TotalAmount)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("total_amount");

            builder.Property(c => c.CouponDiscountAmount)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("coupon_discount_amount");

            builder.Property(c => c.AppliedCouponCode)
                .HasMaxLength(50)
                .HasColumnName("applied_coupon_code");

            builder.Property(c => c.PlatformFee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("platform_fee");

            builder.Property(c => c.SurgeFee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("surge_fee");

            builder.Property(c => c.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active")
                .HasColumnName("status");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(c => c.ExpiresAt)
                .HasColumnName("expires_at");

            builder.HasIndex(c => c.UserId)
                .HasDatabaseName("idx_cart_user_id");

            builder.HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}