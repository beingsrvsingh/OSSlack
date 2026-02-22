using System.Reflection;
using Astrologer.Domain.Entities;
using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AstrologerMicroservice.Infrastructure.Persistence.Context
{
    public class AstrologerDbContext : DbContext
    {
        public AstrologerDbContext(DbContextOptions<AstrologerDbContext> options) : base(options)
        { }

        public DbSet<AstrologerMaster> Astrologers => Set<AstrologerMaster>();
        public DbSet<Schedule> Schedules => Set<Schedule>();

        public DbSet<AstrologerLanguage> AstrologerLanguages => Set<AstrologerLanguage>();
        public DbSet<AstrologerExpertise> AstrologerExpertises => Set<AstrologerExpertise>();
        public DbSet<AstrologerAttributeValue> AstrologerAttributeValues => Set<AstrologerAttributeValue>();
        public DbSet<AstrologerSearchRaw> AstrologerSearchRaws => Set<AstrologerSearchRaw>();
        public DbSet<AstrologerAddon> AstrologerAddons => Set<AstrologerAddon>();
        public DbSet<AstrologerMedia> AstrologerMedias => Set<AstrologerMedia>();
        public DbSet<AstrologerExpertiesMedia> AstrologerExpertiesMedias => Set<AstrologerExpertiesMedia>();

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