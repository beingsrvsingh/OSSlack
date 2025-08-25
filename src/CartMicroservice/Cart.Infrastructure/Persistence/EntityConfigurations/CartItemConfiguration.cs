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

            builder.Property(ci => ci.Id).HasColumnName("id");

            builder.Property(ci => ci.CartId)
                .IsRequired()
                .HasColumnName("cart_id");

            builder.Property(ci => ci.ProductId)
                .IsRequired()
                .HasColumnName("product_id");
            
            builder.Property(ci => ci.ProviderType)
                .IsRequired()
                .HasColumnName("product_type");

            builder.Property(ci => ci.SubCategoryId)
                .HasColumnName("sub_category_id");

            builder.Property(ci => ci.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

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

            builder.Property(ci => ci.SelectedOptionsJson)
                .HasColumnType("jsonb")
                .HasColumnName("custom_options_json");

            builder.Property(ci => ci.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(ci => ci.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(ci => ci.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            builder.HasIndex(ci => ci.CartId).HasDatabaseName("idx_cart_item_cart_id");
            builder.HasIndex(ci => ci.ProductId).HasDatabaseName("idx_cart_item_product_id");

            builder.HasOne<Cart>()
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}