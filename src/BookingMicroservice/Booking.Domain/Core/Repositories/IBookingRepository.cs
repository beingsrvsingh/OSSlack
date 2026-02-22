using BookingMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace BookingMicroservice.Domain.Repositories
{
    public interface IBookingRepository : IRepository<BookingMaster>
    {
        Task<IEnumerable<BookingMaster>> GetAvailableAsync(int bookingId);
    }
}