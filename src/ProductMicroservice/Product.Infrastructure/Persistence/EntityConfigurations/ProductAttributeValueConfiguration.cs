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

            builder.Property(pav => pav.ProductMasterId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.HasOne(pav => pav.ProductMaster)
                .WithMany(p => p.AttributeValues)
                .HasForeignKey(pav => pav.ProductMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(pav => pav.CatalogAttributeId)
                .HasColumnName("catalog_attribute_id")
                .IsRequired();

             builder.Property(pav => pav.CatalogAttributeValueId)
                .HasColumnName("cat_attr_val_id")
                .IsRequired();

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
                .IsRequired();

            builder.Property(pav => pav.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }

    }

}