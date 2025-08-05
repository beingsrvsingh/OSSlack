using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("product_items");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.Property(p => p.AstrologerId)
                   .HasColumnName("astrologer_id");

            builder.Property(p => p.Name)
                   .HasColumnName("name")
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasColumnName("description");

            builder.Property(p => p.Price)
                   .HasColumnName("price");

            builder.HasIndex(p => p.AstrologerId)
                   .HasDatabaseName("ix_product_items_astrologer");

            builder.HasOne(p => p.Astrologer)
                   .WithMany(a => a.ProductItems)
                   .HasForeignKey(p => p.AstrologerId);
        }
    }

}