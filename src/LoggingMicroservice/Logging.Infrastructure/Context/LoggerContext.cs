using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Logging.Infrastructure.Context
{
    public class LoggerContext : DbContext
    {
        public LoggerContext(DbContextOptions<LoggerContext> options) : base(options)
        { }

        public DbSet<Log> Log => Set<Log>();
        public DbSet<AppsLog> AppsLog => Set<AppsLog>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
