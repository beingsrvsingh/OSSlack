using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class CategoryMasterConfiguration : IEntityTypeConfiguration<CategoryMaster>
    {
        public void Configure(EntityTypeBuilder<CategoryMaster> builder)
        {
            builder.ToTable("category_master");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            builder.Property(c => c.CategoryType).HasColumnName("category_type").IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasColumnName("description").HasMaxLength(500);
            builder.Property(c => c.ImageUrl).HasColumnName("image_url").HasMaxLength(300);
            builder.Property(c => c.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(c => c.DisplayOrder).HasColumnName("display_order").HasDefaultValue(0);

            builder.HasMany(c => c.SubCategoryMasters)
                   .WithOne(s => s.CategoryMaster)
                   .HasForeignKey(s => s.CategoryMasterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Localizations)
                   .WithOne()
                   .HasForeignKey(l => l.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);            
        }
    }

}