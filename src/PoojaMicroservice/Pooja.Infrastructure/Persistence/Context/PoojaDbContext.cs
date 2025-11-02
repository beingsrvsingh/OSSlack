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
        public DbSet<PoojaAddon> PoojaAddons => Set<PoojaAddon>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
