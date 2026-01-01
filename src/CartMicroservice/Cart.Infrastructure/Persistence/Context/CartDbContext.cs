using System.Reflection;
using CartMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartMicroservice.Infrastructure.Persistence.Context
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
        }

        public DbSet<CartMicroservice.Domain.Entities.Cart> Carts => Set<CartMicroservice.Domain.Entities.Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}