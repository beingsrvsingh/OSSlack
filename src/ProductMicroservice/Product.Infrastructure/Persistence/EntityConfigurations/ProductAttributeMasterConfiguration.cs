using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductAttributeMasterConfiguration : IEntityTypeConfiguration<ProductAttributeMaster>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeMaster> builder)
        {
            builder.ToTable("product_attribute_master");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id");

            builder.Property(a => a.Key)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("key");

            builder.Property(a => a.Value)
                .HasMaxLength(150)
                .HasColumnName("value");

            builder.Property(a => a.ProductId)
                .HasColumnName("product_id");

            builder.HasOne(a => a.ProductMaster)
                .WithMany(p => p.ProductAttributeMasters)
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}