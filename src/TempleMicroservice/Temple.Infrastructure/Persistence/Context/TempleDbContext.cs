using System.Reflection;
using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TempleMicroservice.Domain.Entities;


namespace Temple.Infrastructure.Persistence.Context
{
    public class TempleDbContext : DbContext
    {
        public TempleDbContext(DbContextOptions<TempleDbContext> options) : base(options)
        { }

        public DbSet<TempleMaster> TempleMasters => Set<TempleMaster>();
        public DbSet<TempleExpertise> TempleExpertises => Set<TempleExpertise>();
        public DbSet<AttributeValue> AttributeValues => Set<AttributeValue>();
        public DbSet<TempleSchedule> TempleSchedules => Set<TempleSchedule>();
        public DbSet<TempleException> TempleExceptions => Set<TempleException>();
        public DbSet<TempleTimeSlot> TempleTimeSlots => Set<TempleTimeSlot>();
        public DbSet<SearchRaw> SearchRaws => Set<SearchRaw>();
        public DbSet<TempleAddon> TempleAddons => Set<TempleAddon>();
        public DbSet<TempleImage> TempleImages => Set<TempleImage>();
        public DbSet<TempleExpertiseImage> templeExpertiseImages => Set<TempleExpertiseImage>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SearchRaw>()
            .HasNoKey()
            .ToView(null);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}