using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class CategoryAttributeGroupMappingConfiguration : IEntityTypeConfiguration<CategoryAttributeGroupMapping>
    {
        public void Configure(EntityTypeBuilder<CategoryAttributeGroupMapping> builder)
        {
            builder.ToTable("attribute_group_mapping");

            builder.HasKey(cagm => cagm.Id);

            builder.Property(cagm => cagm.Id)
                .HasColumnName("id");

            builder.Property(cagm => cagm.CategoryMasterId)
                .HasColumnName("category_id")
                .IsRequired(false);

            builder.Property(cagm => cagm.SubCategoryMasterId)
                .HasColumnName("sub_category_id")
                .IsRequired(false);

            builder.Property(cagm => cagm.AttributeGroupId)
                .HasColumnName("attribute_group_id")
                .IsRequired();

            builder.Property(cagm => cagm.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.HasOne(cagm => cagm.AttributeGroup)
                .WithMany() // Assuming no navigation collection on CatalogAttributeGroupMaster
                .HasForeignKey(cagm => cagm.AttributeGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}