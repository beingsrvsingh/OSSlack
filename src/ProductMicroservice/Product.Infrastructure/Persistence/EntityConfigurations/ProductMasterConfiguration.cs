using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductMasterConfiguration : IEntityTypeConfiguration<ProductMaster>
    {
        public void Configure(EntityTypeBuilder<ProductMaster> builder)
        {
            builder.ToTable("product_master");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.Property(p => p.SubCategoryId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(p => p.ThumbnailUrl)
                .HasMaxLength(300)
                .HasColumnName("thumbnail_url");   

            builder.Property(p => p.IsActive)
                .HasMaxLength(50)
                .HasColumnName("is_active");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("updated_at");

            builder.Property(p => p.Rating)
                .HasColumnName("rating_snap");

            builder.Property(p => p.Reviews)
                .HasColumnName("reviews_snap");

            builder.Property(p => p.CategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("category_name_snapshot");

            builder.Property(p => p.SubCategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sub_category_name_snapshot");            

            builder.Property(p => p.IsTrending)
                .HasColumnName("is_trending");

            builder.Property(p => p.IsFeatured)
                .HasColumnName("is_featured");

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");

            // Relationships
            builder.HasMany(p => p.RegionPriceMaster)
                .WithOne(r => r.ProductMaster)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.VariantMasters)
                .WithOne(v => v.ProductMaster)
                .HasForeignKey(v => v.ProductMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.LocalizationMasters)
                .WithOne(l => l.ProductMaster)
                .HasForeignKey(l => l.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductTagMasters)
                .WithOne(t => t.ProductMaster)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.SEOInfoMaster)
                .WithOne(s => s.ProductMaster)
                .HasForeignKey<ProductSEOInfoMaster>(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.AttributeValues)
                .WithOne(s => s.ProductMaster)
                .HasForeignKey(t => t.ProductMasterId)
                .OnDelete(DeleteBehavior.Cascade);                           
                
            builder.HasMany(p => p.ProductImages)
                .WithOne(img => img.Product)
                .HasForeignKey(img => img.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}