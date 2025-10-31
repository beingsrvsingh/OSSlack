using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductVariantImageConfiguration : IEntityTypeConfiguration<ProductVariantImage>
    {
        public void Configure(EntityTypeBuilder<ProductVariantImage> builder)
        {
            builder.ToTable("product_variant_image");

            builder.HasKey(vi => vi.Id);

            builder.Property(vi => vi.Id)
                .HasColumnName("id");

            builder.Property(vi => vi.ImageUrl)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("image_url");

            builder.Property(vi => vi.SortOrder)
                .HasColumnName("sort_order");

            builder.Property(vi => vi.AltText)
                .HasMaxLength(50)
                .HasColumnName("alt_text");

            builder.Property(vi => vi.ProductVariantId)
                .IsRequired()
                .HasColumnName("product_variant_id");

            builder.HasOne(vi => vi.ProductVariant)
                .WithMany(v => v.VariantImages)
                .HasForeignKey(vi => vi.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
