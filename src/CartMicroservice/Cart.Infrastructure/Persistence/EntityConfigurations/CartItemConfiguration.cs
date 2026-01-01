using CartMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_item");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .HasColumnName("id");

            builder.Property(ci => ci.CartId)
                .IsRequired()
                .HasColumnName("cart_id");

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ci => ci.ProductVariantId)
                .IsRequired()
                .HasColumnName("product_variant_id");

            builder.Property(ci => ci.SubCategoryId)
                .HasColumnName("sub_category_id");

            builder.Property(ci => ci.ProviderType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasColumnName("provider_type");

            builder.Property(ci => ci.ItemNameSnapshot)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("item_name_snapshot");

            builder.Property(ci => ci.SkuSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sku_snapshot");

            builder.Property(ci => ci.PriceSnapshot)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("price_snapshot");

            builder.Property(ci => ci.DiscountAmount)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("discount_amount");

            builder.Property(ci => ci.TaxAmount)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("tax_amount");

            builder.Property(ci => ci.PlatformFee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("platform_fee");

            builder.Property(ci => ci.SurgeFee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("surge_fee");

            builder.Property(ci => ci.AdditionalFees)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("additional_fees");

            builder.Property(ci => ci.Quantity)
                .IsRequired()
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            builder.Property(ci => ci.IsInStock)
                .HasDefaultValue(true)
                .HasColumnName("is_in_stock");

            builder.Property(ci => ci.IsGift)
                .HasDefaultValue(false)
                .HasColumnName("is_gift");

            builder.Property(ci => ci.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            builder.Property(ci => ci.AppliedCouponCode)
                .HasMaxLength(50)
                .HasColumnName("applied_coupon_code");

            builder.Property(ci => ci.GiftMessage)
                .HasMaxLength(250)
                .HasColumnName("gift_message");

            builder.Property(ci => ci.PreferredServiceDateTime)
                .HasColumnName("preferred_service_datetime");

            builder.Property(ci => ci.SelectedOptionsJson)
                .HasColumnType("json")
                .HasColumnName("custom_options_json");

            builder.Property(ci => ci.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("added_at");

            builder.Property(ci => ci.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(ci => ci.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasIndex(ci => ci.CartId)
                .HasDatabaseName("idx_cart_item_cart_id");

            builder.HasIndex(ci => ci.ProductVariantId)
                .HasDatabaseName("idx_cart_item_product_variant_id");
        }
    }


}