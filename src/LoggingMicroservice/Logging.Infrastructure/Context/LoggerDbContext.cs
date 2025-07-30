using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Logging.Infrastructure.Context
{
    public class LoggerDbContext : DbContext
    {
        public LoggerDbContext(DbContextOptions<LoggerDbContext> options) : base(options)
        { }

        public DbSet<AndroidLog> AndroidLogs { get; set; } = null!;
        public DbSet<IOSLog> IOSLogs { get; set; } = null!;
        public DbSet<WebServiceLog> WebServiceLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
