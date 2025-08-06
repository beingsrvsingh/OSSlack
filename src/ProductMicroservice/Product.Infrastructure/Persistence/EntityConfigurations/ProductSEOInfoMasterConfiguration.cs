using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductSEOInfoMasterConfiguration : IEntityTypeConfiguration<ProductSEOInfoMaster>
    {
        public void Configure(EntityTypeBuilder<ProductSEOInfoMaster> builder)
        {
            builder.ToTable("product_seo_info_master");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id");

            builder.Property(s => s.Slug)
                .HasMaxLength(150)
                .HasColumnName("slug");

            builder.Property(s => s.MetaTitle)
                .HasMaxLength(150)
                .HasColumnName("meta_title");

            builder.Property(s => s.MetaDescription)
                .HasMaxLength(300)
                .HasColumnName("meta_description");

            builder.Property(s => s.ProductId)
                .HasColumnName("product_id");

            builder.HasOne(s => s.ProductMaster)
                .WithOne(p => p.SEOInfoMaster)
                .HasForeignKey<ProductSEOInfoMaster>(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}