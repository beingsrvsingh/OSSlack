using Microsoft.EntityFrameworkCore;
using PriestMicroservice.Domain.Entities;
using System.Reflection;

namespace Priest.Infrastructure.Persistence.Context
{
    public class PriestDbContext : DbContext
    {
        public PriestDbContext(DbContextOptions<PriestDbContext> options)
            : base(options)
        {
        }

        public DbSet<PriestMaster> PriestMasters => Set<PriestMaster>();
        public DbSet<AttributeValue> AttributeValues => Set<AttributeValue>();
        public DbSet<ConsultationMode> ConsultationModes => Set<ConsultationMode>();
        public DbSet<ConsultationModeMaster> ConsultationModeMasters => Set<ConsultationModeMaster>();
        public DbSet<PriestExpertise> PriestExpertise => Set<PriestExpertise>();
        public DbSet<PriestLanguage> PriestLanguages => Set<PriestLanguage>();
        public DbSet<LanguageMaster> LanguageMasters => Set<LanguageMaster>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<SearchRaw> SearchRaws => Set<SearchRaw>();

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