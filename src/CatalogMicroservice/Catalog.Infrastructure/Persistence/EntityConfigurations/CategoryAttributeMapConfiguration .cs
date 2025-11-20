using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class CategoryAttributeMapConfiguration : IEntityTypeConfiguration<CatalogAttributeMap>
    {
        public void Configure(EntityTypeBuilder<CatalogAttributeMap> builder)
        {
            builder.ToTable("category_attribute_map");

            builder.HasKey(cam => cam.Id);

            builder.Property(cam => cam.Id)
                .HasColumnName("id");

            builder.Property(cam => cam.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            builder.Property(cam => cam.SubCategoryId)
                .HasColumnName("sub_category_id")
                .IsRequired();

            builder.Property(cam => cam.AttributeId)
                .HasColumnName("attribute_id")
                .IsRequired();

            builder.Property(cam => cam.AttributeGroupId)
                .HasColumnName("attribute_group_id")
                .IsRequired();

            builder.Property(cam => cam.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(cam => cam.IsRequired)
                .HasColumnName("is_required")
                .HasDefaultValue(false);

            builder.Property(cam => cam.IsFilterable)
                .HasColumnName("is_filterable")
                .HasDefaultValue(false);

            builder.Property(cam => cam.IsSummary)
                .HasColumnName("is_summary")
                .HasDefaultValue(false);

            builder.Property(cam => cam.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(cam => cam.Attribute)
                .WithMany()
                .HasForeignKey(cam => cam.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cam => cam.AttributeGroup)
                .WithMany()
                .HasForeignKey(cam => cam.AttributeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
