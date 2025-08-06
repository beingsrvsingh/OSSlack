using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductTagMasterConfiguration : IEntityTypeConfiguration<ProductTagMaster>
    {
        public void Configure(EntityTypeBuilder<ProductTagMaster> builder)
        {
            builder.ToTable("product_tag_master");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.Tag)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("tag");

            builder.Property(t => t.ProductId)
                .HasColumnName("product_id");

            builder.HasOne(t => t.ProductMaster)
                .WithMany(p => p.ProductTagMasters)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}