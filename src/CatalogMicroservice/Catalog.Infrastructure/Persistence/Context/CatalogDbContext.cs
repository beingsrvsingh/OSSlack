using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Persistence.Context;

public partial class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options) { }

    // Category
    public DbSet<CategoryMaster> Categories => Set<CategoryMaster>();
    public DbSet<CategoryLocalizedText> CategoryLocalizedTexts => Set<CategoryLocalizedText>();

    // SubCategory
    public DbSet<SubCategoryMaster> SubCategories => Set<SubCategoryMaster>();
    public DbSet<SubCategoryLocalizedText> SubCategoryLocalizedTexts => Set<SubCategoryLocalizedText>();

    // Pooja Kits
    public DbSet<PoojaKitMaster> PoojaKits => Set<PoojaKitMaster>();
    public DbSet<PoojaKitItemMaster> PoojaKitItems => Set<PoojaKitItemMaster>();
    public DbSet<PoojaKitLocalizedText> PoojaKitLocalizedTexts => Set<PoojaKitLocalizedText>();
    public DbSet<PoojaKitItemLocalizedText> PoojaKitItemTags => Set<PoojaKitItemLocalizedText>();

    public DbSet<PoojaMaster> PoojaMasters => Set<PoojaMaster>();
    public DbSet<PoojaLocalizedText> PoojaLocalizedTexts  => Set<PoojaLocalizedText>();
    public DbSet<PoojaTag> PoojaTags  => Set<PoojaTag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
