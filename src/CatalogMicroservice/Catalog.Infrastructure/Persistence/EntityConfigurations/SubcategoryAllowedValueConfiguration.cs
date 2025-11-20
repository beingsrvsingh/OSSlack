using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class SubcategoryAllowedValueConfiguration : IEntityTypeConfiguration<SubcategoryAllowedValue>
    {
        public void Configure(EntityTypeBuilder<SubcategoryAllowedValue> builder)
        {
            builder.ToTable("subcategory_allowed_value");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SubCategoryId)
                .HasColumnName("sub_category_id")
                .IsRequired();

            builder.Property(e => e.AttributeId)
                .HasColumnName("attribute_id")
                .IsRequired();

            builder.Property(e => e.AllowedValueId)
                .HasColumnName("allowed_value_id")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired();

            // Optional: Foreign keys
            builder.HasOne(e => e.SubCategoryMaster)
                .WithMany()
                .HasForeignKey(e => e.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Attribute)
                .WithMany()
                .HasForeignKey(e => e.AttributeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CatalogAttributeAllowedValue)
                .WithMany()
                .HasForeignKey(e => e.AllowedValueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional: Unique constraint (subcategory + attribute + allowed_value)
            builder.HasIndex(e => new { e.SubCategoryId, e.AttributeId, e.AllowedValueId })
                   .IsUnique();
        }
    }
}
