using System.Reflection;
using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Temple.Infrastructure.Persistence.Context
{
    public class TempleDbContext : DbContext
    {
        public TempleDbContext(DbContextOptions<TempleDbContext> options) : base(options)
        { }

        public DbSet<TempleMaster> TempleMasters => Set<TempleMaster>();
        public DbSet<TempleSchedule> TempleSchedules => Set<TempleSchedule>();
        public DbSet<TempleException> TempleExceptions => Set<TempleException>();
        public DbSet<TemplePooja> TemplePoojas => Set<TemplePooja>();
        public DbSet<TemplePrasad> Prasads => Set<TemplePrasad>();
        public DbSet<TempleDonation> Donations => Set<TempleDonation>();
        public DbSet<TempleAarti> Aartis => Set<TempleAarti>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}