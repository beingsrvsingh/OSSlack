using BookingMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace BookingMicroservice.Domain.Repositories
{
    public interface IBookingRepository : IRepository<BookingMaster>
    {
        Task<List<BookingMaster>>GetBookingsByDateAsync(int entityId, DateTime date);
    }
}