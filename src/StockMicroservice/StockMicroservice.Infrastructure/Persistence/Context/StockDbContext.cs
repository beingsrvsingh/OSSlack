using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StockMicroservice.Domain.Entities;

namespace StockMicroservice.Infrastructure.Persistence.Context
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }

        public DbSet<WarehouseMaster> Warehouses => Set<WarehouseMaster>();
        public DbSet<StockMaster> Stocks => Set<StockMaster>();
        public DbSet<StockTransactionLog> StockTransactionLogs => Set<StockTransactionLog>();
        public DbSet<StockAlert> StockAlerts => Set<StockAlert>();
        public DbSet<StockReservation> StockReservations => Set<StockReservation>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}