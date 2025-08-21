
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

            builder.Property(ca => ca.DataType)
                .IsRequired()
                .HasColumnName("data_type");

            builder.Property(ca => ca.IsCustom)
                .HasColumnName("is_custom")
                .HasDefaultValue(false);

            builder.Property(ca => ca.IsRequired)
                .HasColumnName("is_required")
                .HasDefaultValue(false);

            builder.Property(ca => ca.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(ca => ca.SubCategoryMasterId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.HasOne(ca => ca.SubCategoryMaster)
                .WithMany(c => c.CatalogAttributes)
                .HasForeignKey(ca => ca.SubCategoryMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ca => ca.AllowedValues)
                .WithOne(av => av.CatalogAttribute)
                .HasForeignKey(av => av.CatalogAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ca => ca.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(ca => ca.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);
        }
    }

}