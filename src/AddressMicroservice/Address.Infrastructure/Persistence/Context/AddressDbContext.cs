using System.Reflection;
using Address.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Address.Infrastructure.Persistence
{
    public class AddressDbContext : DbContext
    {
        public AddressDbContext(DbContextOptions<AddressDbContext> options) : base(options)
        { }

        public DbSet<AddressEntity> Addresses => Set<AddressEntity>();
        public DbSet<AddressType> AddressTypes => Set<AddressType>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}