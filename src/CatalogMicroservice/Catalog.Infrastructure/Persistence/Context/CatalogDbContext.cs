using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Persistence.Context;

public partial class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{

    // Category
    public DbSet<CategoryMaster> Categories => Set<CategoryMaster>();
    public DbSet<CategoryLocalizedText> CategoryLocalizedTexts => Set<CategoryLocalizedText>();

    // SubCategory
    public DbSet<SubCategoryMaster> SubCategories => Set<SubCategoryMaster>();
    public DbSet<SubCategoryLocalizedText> SubCategoryLocalizedTexts => Set<SubCategoryLocalizedText>();
    public DbSet<PoojaKitItemMaster> PoojaKitItems => Set<PoojaKitItemMaster>();
    public DbSet<CatalogAttribute> CatalogAttributes => Set<CatalogAttribute>();
    public DbSet<CatalogAttributeAllowedValue> CatalogAttributeAllowedValues => Set<CatalogAttributeAllowedValue>();
    public DbSet<CatalogAttributeIcon> CatalogAttributeIcons => Set<CatalogAttributeIcon>();
    public DbSet<CatalogAttributeGroupMaster> CatalogAttributeGroupMasters => Set<CatalogAttributeGroupMaster>();
    public DbSet<CategoryAttributeGroupMapping> CategoryAttributeGroupMappings => Set<CategoryAttributeGroupMapping>();
    public DbSet<CatalogAttributeRaw> RawAttributeValues => Set<CatalogAttributeRaw>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CategoryMaster>().HasData(
        new CategoryMaster { Id = 1, ParentCategoryId = null, CategoryType = "Product", Name = "Product", Description = "All tangible products", DisplayOrder = 1, ImageUrl = "https://example.com/images/product_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 2, ParentCategoryId = null, CategoryType = "Service", Name = "Service", Description = "All service-based offerings", DisplayOrder = 2, ImageUrl = "https://example.com/images/service_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 3, ParentCategoryId = null, CategoryType = "Resource", Name = "Resource", Description = "All human or spiritual resources", DisplayOrder = 3, ImageUrl = "https://example.com/images/resource_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 101, ParentCategoryId = 1, CategoryType = "Product", Name = "Electronics", Description = "Electronic items like phones and gadgets", DisplayOrder = 1, ImageUrl = "https://example.com/images/electronics_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 102, ParentCategoryId = 1, CategoryType = "Product", Name = "Clothing", Description = "Men and women clothing", DisplayOrder = 2, ImageUrl = "https://example.com/images/clothing_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 103, ParentCategoryId = 2, CategoryType = "Service", Name = "Pooja", Description = "Religious pooja services and rituals", DisplayOrder = 1, ImageUrl = "https://example.com/images/pooja_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 104, ParentCategoryId = 2, CategoryType = "Service", Name = "Temple", Description = "Religious temples and associated services", DisplayOrder = 2, ImageUrl = "https://example.com/images/temple_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 105, ParentCategoryId = 2, CategoryType = "Service", Name = "Astrologer", Description = "Astrology services and consultations", DisplayOrder = 3, ImageUrl = "https://example.com/images/astrologer_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 106, ParentCategoryId = 2, CategoryType = "Service", Name = "Kathavachak", Description = "Religious storytellers and spiritual discourses", DisplayOrder = 4, ImageUrl = "https://example.com/images/kathavachak_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        new CategoryMaster { Id = 107, ParentCategoryId = 3, CategoryType = "Resource", Name = "Priest", Description = "Priests available for rituals and poojas", DisplayOrder = 1, ImageUrl = "https://example.com/images/priest_icon.png", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });

        builder.Entity<AttributeDataTypeMaster>().HasData(
        new AttributeDataTypeMaster { Id = 1, Name = "String" },
        new AttributeDataTypeMaster { Id = 2, Name = "Integer" },
        new AttributeDataTypeMaster { Id = 3, Name = "Decimal" },
        new AttributeDataTypeMaster { Id = 4, Name = "Boolean" },
        new AttributeDataTypeMaster { Id = 5, Name = "Date" },
        new AttributeDataTypeMaster { Id = 6, Name = "Time" },
        new AttributeDataTypeMaster { Id = 7, Name = "DateTime" },
        new AttributeDataTypeMaster { Id = 8, Name = "Multiselect" },
        new AttributeDataTypeMaster { Id = 9, Name = "Dropdown" });

        builder.Entity<CatalogAttributeGroupMaster>().HasData(
        new CatalogAttributeGroupMaster { Id = 1, GroupKey = "basic_info", DisplayName = "Basic Information", SortOrder = 1, IsActive = true },
        new CatalogAttributeGroupMaster { Id = 2, GroupKey = "technical_specs", DisplayName = "Technical Specifications", SortOrder = 2, IsActive = true },
        new CatalogAttributeGroupMaster { Id = 3, GroupKey = "variant_info", DisplayName = "Variant Details", SortOrder = 3, IsActive = true });

        builder.Entity<CategoryAttributeGroupMapping>().HasData(
        new CategoryAttributeGroupMapping { Id = 1, CategoryMasterId = 1, SubCategoryMasterId = 1001, AttributeGroupId = 1, SortOrder = 1 }, // Basic Info
        new CategoryAttributeGroupMapping { Id = 2, CategoryMasterId = 1, SubCategoryMasterId = 1001, AttributeGroupId = 2, SortOrder = 2 }, // Technical Specs
        new CategoryAttributeGroupMapping { Id = 3, CategoryMasterId = 1, SubCategoryMasterId = 1001, AttributeGroupId = 3, SortOrder = 3 }  // Variant Info
        );

        // Optional, but avoids EF assuming a table
        builder.Entity<CatalogAttributeRaw>()
        .HasNoKey()
        .ToView(null);


        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
