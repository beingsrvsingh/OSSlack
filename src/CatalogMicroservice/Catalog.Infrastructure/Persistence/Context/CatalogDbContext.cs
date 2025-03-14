using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Persistence.Context;

public partial class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options) { }

    public virtual DbSet<CategoryMaster> CategoryMasters => Set<CategoryMaster>();

    public virtual DbSet<SubCategoryMaster> SubCategoryMasters => Set<SubCategoryMaster>();

    public virtual DbSet<ChildSubCategoryMaster> ChildSubCategoryMasters => Set<ChildSubCategoryMaster>();    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
