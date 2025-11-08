using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaAttributeValueConfiguration : IEntityTypeConfiguration<PoojaAttributeValue>
    {
        public void Configure(EntityTypeBuilder<PoojaAttributeValue> builder)
        {
            builder.ToTable("pooja_attribute_value");

            builder.HasKey(pav => pav.Id);

            builder.Property(pav => pav.Id)
                .HasColumnName("id");

            // ProductMaster relationship
            builder.Property(pav => pav.PoojaMasterId)
                .HasColumnName("pooja_id");            

            // ProductVariant relationship (optional)
            builder.Property(pav => pav.PoojaVariantId)
                .HasColumnName("pooja_variant_id");

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
                .HasMaxLength(500);

            builder.Property(pav => pav.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(pav => pav.PoojaMaster)
                .WithMany(p => p.PoojaAttributeValues)
                .HasForeignKey(pav => pav.PoojaMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pav => pav.PoojaVariantMaster)
                .WithMany(v => v.PoojaAttributeValues)
                .HasForeignKey(pav => pav.PoojaVariantId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}