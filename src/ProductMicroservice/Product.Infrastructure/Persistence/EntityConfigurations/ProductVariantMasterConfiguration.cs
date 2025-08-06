using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductVariantMasterConfiguration : IEntityTypeConfiguration<ProductVariantMaster>
    {
        public void Configure(EntityTypeBuilder<ProductVariantMaster> builder)
        {
            builder.ToTable("product_variant_master");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(v => v.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            builder.Property(v => v.ProductId)
                .HasColumnName("product_id");

            builder.HasOne(v => v.ProductMaster)
                .WithMany(p => p.VariantMasters)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}