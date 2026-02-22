using BookingMicroservice.Domain.Entities;
using BookingMicroservice.Domain.Repositories;
using BookingMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Polly;
using Shared.Domain.Entities.Base;
using Shared.Infrastructure.Repositories;

namespace BookingMicroservice.Infrastructure.Persistence.Repository
{
    public class BookingRepository : Repository<BookingMaster>, IBookingRepository
    {
        private readonly BookingDbContext _context;
        public BookingRepository(BookingDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<BookingMaster>> GetAvailableAsync(int bookingId)
        {
            return await _context.Bookings
            .Where(a => a.Id == bookingId)
            .ToListAsync();
        }
    }
}