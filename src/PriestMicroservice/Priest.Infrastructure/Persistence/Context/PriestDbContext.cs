using Microsoft.EntityFrameworkCore;
using Priest.Domain.Entities;
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
        public DbSet<ConsultationMode> ConsultationModes => Set<ConsultationMode>();
        public DbSet<PriestExpertise> PriestExpertise => Set<PriestExpertise>();
        public DbSet<PriestLanguage> PriestLanguages => Set<PriestLanguage>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}