using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductMaster> ProductMasters { get; set; } = null!;
        public DbSet<ProductRegionPriceMaster> ProductRegionPriceMasters { get; set; } = null!;
        public DbSet<ProductVariantMaster> ProductVariantMasters { get; set; } = null!;
        public DbSet<LocalizedProductInfoMaster> LocalizedProductInfoMasters { get; set; } = null!;
        public DbSet<ProductTagMaster> ProductTagMasters { get; set; } = null!;
        public DbSet<ProductSEOInfoMaster> ProductSEOInfoMasters { get; set; } = null!;
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<ProductFilterRawResult> ProductFilterRawResults => Set<ProductFilterRawResult>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}