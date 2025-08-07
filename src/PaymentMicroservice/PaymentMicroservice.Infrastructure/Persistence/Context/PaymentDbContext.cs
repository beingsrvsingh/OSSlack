using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Domain.Entities;

namespace PaymentMicroservice.Infrastructure.Persistence.Context
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
        public DbSet<PaymentTransactionLog> PaymentTransactionLogs => Set<PaymentTransactionLog>();
        public DbSet<RefundTransaction> RefundTransactions => Set<RefundTransaction>();
        public DbSet<PaymentMethodDetails> PaymentMethodDetails => Set<PaymentMethodDetails>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}