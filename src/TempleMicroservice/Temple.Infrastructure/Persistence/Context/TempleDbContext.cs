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
        public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();
        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Expertise> Expertises => Set<Expertise>();

        public DbSet<AstrologerLanguage> AstrologerLanguages => Set<AstrologerLanguage>();
        public DbSet<TempleExpertise> TempleExpertises => Set<TempleExpertise>();
        public DbSet<ServiceTagPackage> ServiceTagPackages => Set<ServiceTagPackage>();
        public DbSet<ServiceTagPackageMaster> TagMasters => Set<ServiceTagPackageMaster>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}