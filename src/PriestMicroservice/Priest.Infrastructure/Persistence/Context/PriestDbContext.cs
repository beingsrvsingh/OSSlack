using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Priest.Infrastructure.Persistence.Context
{
    public class PriestDbContext : DbContext
    {
        public PriestDbContext(DbContextOptions<PriestDbContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}