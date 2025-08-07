using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubCategoryMasterConfiguration : IEntityTypeConfiguration<SubCategoryMaster>
{
    public void Configure(EntityTypeBuilder<SubCategoryMaster> builder)
    {
        builder.ToTable("sub_category_master");

        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).HasColumnName("id");

        builder.Property(sc => sc.CategoryMasterId)
            .IsRequired()
            .HasColumnName("category_master_id");

        builder.Property(sc => sc.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(sc => sc.Description)
            .HasColumnName("description");

        builder.Property(sc => sc.IsActive)
            .HasDefaultValue(true)
            .HasColumnName("is_active");

        builder.Property(sc => sc.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
            .HasColumnName("created_at");

        builder.Property(sc => sc.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasOne(sc => sc.CategoryMaster)
            .WithMany(c => c.SubCategoryMasters)
            .HasForeignKey(sc => sc.CategoryMasterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(sc => sc.PoojaKits)
               .WithOne(pk => pk.SubCategoryMaster)
               .HasForeignKey(pk => pk.SubCategoryMasterId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(sc => sc.Localizations)
               .WithOne(l => l.SubCategory)
               .HasForeignKey(l => l.SubCategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}