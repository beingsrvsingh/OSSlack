using BookingMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Service
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponseDto>> GetBookingsByDateAsync(int entityId, DateTime date);

        Task<IEnumerable<BookingMaster>> GetBookingByIdAsync(int bookingId);

        Task<string> CreateAsync(BookingMaster booking);
    }
}