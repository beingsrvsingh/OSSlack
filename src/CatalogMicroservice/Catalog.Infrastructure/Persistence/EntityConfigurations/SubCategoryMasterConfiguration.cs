using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations;

public class SubCategoryMasterConfiguration : IEntityTypeConfiguration<SubCategoryMaster>
{
       public void Configure(EntityTypeBuilder<SubCategoryMaster> builder)
       {
              builder.ToTable("sub_category_master");

              builder.HasKey(s => s.Id);

              builder.Property(s => s.Id).HasColumnName("id");
              builder.Property(s => s.CategoryMasterId).HasColumnName("category_id");
              builder.Property(s => s.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
              builder.Property(s => s.Description).HasColumnName("description").HasMaxLength(500);
              builder.Property(s => s.IsActive).HasColumnName("is_active").HasDefaultValue(true);
              builder.Property(s => s.SubcategoryType).HasColumnName("subcategory_type").HasConversion<int>();
              builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
              builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").IsRequired(false);

              builder.Property(s => s.ParentSubcategoryId).HasColumnName("parent_subcategory_id");

              builder.HasOne(s => s.CategoryMaster)
                     .WithMany(c => c.SubCategoryMasters)
                     .HasForeignKey(s => s.CategoryMasterId)
                     .OnDelete(DeleteBehavior.Restrict);

              builder.HasOne(s => s.ParentSubcategory)
                     .WithMany(p => p.ChildSubcategories)
                     .HasForeignKey(s => s.ParentSubcategoryId)
                     .OnDelete(DeleteBehavior.Restrict);

              builder.HasMany(s => s.Localizations)
                     .WithOne(s => s.SubCategory)
                     .HasForeignKey(l => l.SubCategoryId)
                     .OnDelete(DeleteBehavior.Cascade);

               builder.HasMany(c => c.CatalogAttributes)
                     .WithOne(ca => ca.SubCategoryMaster)
                     .HasForeignKey(ca => ca.SubCategoryMasterId)
                     .OnDelete(DeleteBehavior.Cascade);

    }
}
