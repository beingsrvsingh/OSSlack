using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<OrderHeader> OrderHeaders => Set<OrderHeader>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<OrderShippingAddress> OrderShippingAddresses => Set<OrderShippingAddress>();
        public DbSet<OrderItemCustomization> OrderItemCustomizations => Set<OrderItemCustomization>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}