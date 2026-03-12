using BookingMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;

namespace BookingMicroservice.Application.Service
{
    public interface IBookingService
    {
        Task<string> CreateAsync(BookingMaster booking);

        Task<IEnumerable<BookingResponseDto>> GetBookingsByDateAsync(int entityId, DateTime date);

        Task<IEnumerable<BookingMaster>> GetBookingByIdAsync(int bookingId);

        Task<bool> UpdateStatusBookingAsync(BookingMaster booking);
    }
}