using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Persistence.Context
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }

        public DbSet<WarehouseMaster> Warehouses => Set<WarehouseMaster>();
        public DbSet<WarehouseTransfer> WarehouseTransfers => Set<WarehouseTransfer>();
        public DbSet<StockMaster> Stocks => Set<StockMaster>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
        public DbSet<StockTransactionLog> StockTransactionLogs => Set<StockTransactionLog>();
        public DbSet<StockAlert> StockAlerts => Set<StockAlert>();
        public DbSet<StockReservation> StockReservations => Set<StockReservation>();
        public DbSet<StockAdjustment> StockAdjustments => Set<StockAdjustment>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}