using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaMasterConfiguration : IEntityTypeConfiguration<PoojaMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaMaster> builder)
        {
            builder.ToTable("pooja_master");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.Property(p => p.SubCategoryId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(p => p.ThumbnailUrl)
                .HasMaxLength(300)
                .HasColumnName("thumbnail_url");

            builder.Property(p => p.IsActive)
                .HasMaxLength(50)
                .HasColumnName("is_active");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("updated_at");

            builder.Property(p => p.Rating)
                .HasColumnName("rating_snap");

            builder.Property(p => p.Reviews)
                .HasColumnName("reviews_snap");

            builder.Property(p => p.CategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("category_name_snapshot");

            builder.Property(p => p.SubCategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sub_category_name_snapshot");

            builder.Property(p => p.IsTrending)
                .HasColumnName("is_trending");

            builder.Property(p => p.IsFeatured)
                .HasColumnName("is_featured");

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");

            // Relationships

            builder.HasMany(p => p.PoojaVariantMasters)
                .WithOne(v => v.PoojaMaster)
                .HasForeignKey(v => v.PoojaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PoojaAttributeValues)
                .WithOne(s => s.PoojaMaster)
                .HasForeignKey(t => t.PoojaMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PoojaImages)
                .WithOne(img => img.PoojaMaster)
                .HasForeignKey(img => img.PoojaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PoojaAddons)
                .WithOne(img => img.PoojaMaster)
                .HasForeignKey(img => img.PoojaId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
