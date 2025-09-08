using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Kathavachak.Infrastructure.Persistence.Context
{
    public class KathavachakDbContext : DbContext
    {
        public KathavachakDbContext(DbContextOptions<KathavachakDbContext> options)
            : base(options)
        { }

        public DbSet<KathavachakMaster> KathavachakMasters => Set<KathavachakMaster>();
        public DbSet<KathavachakExpertise> KathavachakCategories => Set<KathavachakExpertise>();
        public DbSet<KathavachakLanguage> KathavachakLanguages => Set<KathavachakLanguage>();
        public DbSet<KathavachakTopic> KathavachakTopics => Set<KathavachakTopic>();
        public DbSet<KathavachakSessionMode> KathavachakSessionModes => Set<KathavachakSessionMode>();
        public DbSet<KathavachakSchedule> KathavachakSchedules => Set<KathavachakSchedule>();
        public DbSet<KathavachakTimeSlot> KathavachakTimeSlots => Set<KathavachakTimeSlot>();
        public DbSet<KathavachakMedia> KathavachakMedia => Set<KathavachakMedia>();
        public DbSet<LanguageMaster> Languages => Set<LanguageMaster>();
        public DbSet<KathavachakSearchRaw> KathavachakSearchRaws => Set<KathavachakSearchRaw>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<KathavachakSearchRaw>()
            .HasNoKey()
            .ToView(null);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
