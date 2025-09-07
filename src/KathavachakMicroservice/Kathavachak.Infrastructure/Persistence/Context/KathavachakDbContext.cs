using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kathavachak.Infrastructure.Persistence.Context
{
    public class KathavachakDbContext : DbContext
    {
        public KathavachakDbContext(DbContextOptions<KathavachakDbContext> options)
            : base(options)
        { }

        public DbSet<KathavachakMaster> KathavachakMasters => Set<KathavachakMaster>();
        public DbSet<KathavachakCategory> KathavachakCategories => Set<KathavachakCategory>();
        public DbSet<KathavachakLanguage> KathavachakLanguages => Set<KathavachakLanguage>();
        public DbSet<KathavachakTopic> KathavachakTopics => Set<KathavachakTopic>();
        public DbSet<KathavachakSessionMode> KathavachakSessionModes => Set<KathavachakSessionMode>();
        public DbSet<KathavachakSchedule> KathavachakSchedules => Set<KathavachakSchedule>();
        public DbSet<KathavachakTimeSlot> KathavachakTimeSlots => Set<KathavachakTimeSlot>();
        public DbSet<KathavachakMedia> KathavachakMedia => Set<KathavachakMedia>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
