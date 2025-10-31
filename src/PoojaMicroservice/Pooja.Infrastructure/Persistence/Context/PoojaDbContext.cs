using Microsoft.EntityFrameworkCore;
using Pooja.Domain.Entities;
using System.Reflection;

namespace Pooja.Infrastructure.Persistence.Context
{
    public class PoojaDbContext : DbContext
    {
        public PoojaDbContext(DbContextOptions<PoojaDbContext> options)
            : base(options)
        {
        }

        public DbSet<PoojaMaster> PoojaMasters => Set<PoojaMaster>();
        public DbSet<PoojaTemple> PoojaTemples => Set<PoojaTemple>();
        public DbSet<PoojaPriest> PoojaPriests => Set<PoojaPriest>();
        public DbSet<PoojaAddon> PoojaAddons => Set<PoojaAddon>();
        public DbSet<PoojaCategory> PoojaCategories => Set<PoojaCategory>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
