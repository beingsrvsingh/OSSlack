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
    public DbSet<PoojaKitItemMaster> PoojaKitItems => Set<PoojaKitItemMaster>();

    public object SubCategoryMasters { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
