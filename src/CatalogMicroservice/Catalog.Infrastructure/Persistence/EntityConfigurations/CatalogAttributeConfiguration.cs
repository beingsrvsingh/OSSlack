
namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    using Catalog.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CatalogAttributeConfiguration : IEntityTypeConfiguration<CatalogAttribute>
    {
        public void Configure(EntityTypeBuilder<CatalogAttribute> builder)
        {
            builder.ToTable("catalog_attribute");

            builder.HasKey(ca => ca.Id);
            builder.Property(ca => ca.Id)
                .HasColumnName("id");

            builder.Property(ca => ca.Key)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("key");

            builder.Property(ca => ca.Label)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("label");

            builder.Property(ca => ca.AttributeDataTypeId)
                .IsRequired()
                .HasColumnName("attribute_datatype_id");

            builder.Property(ca => ca.AttributeGroupId)
                .HasColumnName("attribute_group_id")
                .IsRequired(false);

            builder.Property(ca => ca.CategoryMasterId)
                .HasColumnName("category_id")
                .IsRequired(false);

            builder.Property(ca => ca.SubCategoryMasterId)
                .HasColumnName("sub_category_id")
                .IsRequired(false);

            builder.Property(ca => ca.AttributeIconId)
                .HasColumnName("attribute_icon_id")
                .IsRequired(false);

            builder.Property(ca => ca.IsCustom)
                .HasColumnName("is_custom")
                .HasDefaultValue(false);

            builder.Property(ca => ca.IsRequired)
                .HasColumnName("is_required")
                .HasDefaultValue(false);

            builder.Property(ca => ca.IsFilterable)
                .HasColumnName("is_filterable")
                .HasDefaultValue(false);

            builder.Property(ca => ca.IsSummary)
                .HasColumnName("is_summary")
                .HasDefaultValue(false);

            builder.Property(ca => ca.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(ca => ca.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(ca => ca.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            // Relationships

            builder.HasOne(ca => ca.AttributeIcon)
                .WithMany() // No navigation property on CatalogAttributeIcon
                .HasForeignKey(ca => ca.AttributeIconId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ca => ca.SubCategoryMaster)
                .WithMany(sc => sc.CatalogAttributes)
                .HasForeignKey(ca => ca.SubCategoryMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.AttributeGroup)
                .WithMany()
                .HasForeignKey(ca => ca.AttributeGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ca => ca.AttributeDataType)
                .WithMany(ad => ad.CatalogAttributes)
                .HasForeignKey(ca => ca.AttributeDataTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ca => ca.AllowedValues)
                .WithOne(av => av.CatalogAttribute)
                .HasForeignKey(av => av.CatalogAttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}