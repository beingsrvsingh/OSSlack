using System.Reflection;
using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AstrologerMicroservice.Infrastructure.Persistence.Context
{
    public class AstrologerDbContext : DbContext
    {
        public AstrologerDbContext(DbContextOptions<AstrologerDbContext> options) : base(options)
        { }

        public DbSet<Astrologer> Astrologers => Set<Astrologer>();
        public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();
        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<ProductItem> ProductItems => Set<ProductItem>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Expertise> Expertises => Set<Expertise>();

        public DbSet<AstrologerLanguage> AstrologerLanguages => Set<AstrologerLanguage>();
        public DbSet<AstrologerExpertise> AstrologerExpertises => Set<AstrologerExpertise>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}