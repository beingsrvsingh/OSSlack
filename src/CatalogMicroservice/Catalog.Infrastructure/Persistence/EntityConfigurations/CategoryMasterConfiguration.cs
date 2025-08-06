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

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .HasColumnName("description");

            builder.Property(c => c.DisplayOrder)
                .HasColumnName("display_order");

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(300)
                .HasColumnName("image_url");

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.HasMany(c => c.SubCategoryMasters)
                   .WithOne(sc => sc.CategoryMaster)
                   .HasForeignKey(sc => sc.CategoryMasterId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Localizations)
                   .WithOne(l => l.Category)
                   .HasForeignKey(l => l.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}