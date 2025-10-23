using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> entity)
        {
            entity.ToTable("product_image");

            entity.HasKey(img => img.Id);

            entity.Property(img => img.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(img => img.ImageUrl)
                .HasColumnName("image_url")
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(entity => entity.SortOrder)
                .HasColumnName("sort_order");

            entity.Property(entity => entity.AltText)
                .HasColumnName("alt_text");

            entity.Property(img => img.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            entity.HasOne(img => img.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(img => img.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}