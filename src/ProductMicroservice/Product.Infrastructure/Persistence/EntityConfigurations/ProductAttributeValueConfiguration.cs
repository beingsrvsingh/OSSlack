using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.ToTable("product_attribute_value");

            builder.HasKey(pav => pav.Id);

            builder.Property(pav => pav.Id)
                .HasColumnName("id");

            // ProductMaster relationship
            builder.Property(pav => pav.ProductMasterId)
                .HasColumnName("product_id");

            builder.HasOne(pav => pav.ProductMaster)
                .WithMany(p => p.AttributeValues)
                .HasForeignKey(pav => pav.ProductMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductVariant relationship (optional)
            builder.Property(pav => pav.ProductVariantId)
                .HasColumnName("product_variant_id");

            builder.Property(pav => pav.CatalogAttributeId)
                .HasColumnName("catalog_attribute_id");

            builder.Property(pav => pav.CatalogAttributeValueId)
                .HasColumnName("catalog_attribute_value_id");

            builder.Property(pav => pav.AttributeKey)
                .HasColumnName("attribute_key")
                .HasMaxLength(100);

            builder.Property(pav => pav.AttributeLabel)
                .HasColumnName("attribute_label")
                .HasMaxLength(200);

            builder.Property(pav => pav.AttributeDataTypeId)
                .HasColumnName("attribute_datatype_id");

            builder.Property(pav => pav.CatalogAttributeGroupId)
                .HasColumnName("attribute_group_id");

            builder.Property(pav => pav.Value)
                .HasColumnName("value")
                .HasMaxLength(500); // optional, depends on attribute type

            builder.Property(pav => pav.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(pav => pav.ProductVariant)
                .WithMany(v => v.Attributes)
                .HasForeignKey(pav => pav.ProductVariantId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}