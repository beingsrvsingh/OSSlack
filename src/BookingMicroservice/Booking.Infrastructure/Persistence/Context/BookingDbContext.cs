using BookingMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace BookingMicroservice.Infrastructure.Persistence.Context
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        { }

        public DbSet<BookingMaster> Bookings => Set<BookingMaster>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}