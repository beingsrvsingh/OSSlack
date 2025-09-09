using System.Reflection;
using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AstrologerMicroservice.Infrastructure.Persistence.Context
{
    public class AstrologerDbContext : DbContext
    {
        public AstrologerDbContext(DbContextOptions<AstrologerDbContext> options) : base(options)
        { }

        public DbSet<AstrologerEntity> Astrologers => Set<AstrologerEntity>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<LanguageMaster> Languages => Set<LanguageMaster>();
        public DbSet<AstrologerExpertise> Expertises => Set<AstrologerExpertise>();

        public DbSet<AstrologerLanguage> AstrologerLanguages => Set<AstrologerLanguage>();
        public DbSet<AstrologerExpertise> AstrologerExpertises => Set<AstrologerExpertise>();
        public DbSet<AstrologerAttributeValue> AstrologerAttributeValues => Set<AstrologerAttributeValue>();
        public DbSet<AstrologerSearchRaw> AstrologerSearchRaws => Set<AstrologerSearchRaw>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AstrologerSearchRaw>()
            .HasNoKey()
            .ToView(null);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}