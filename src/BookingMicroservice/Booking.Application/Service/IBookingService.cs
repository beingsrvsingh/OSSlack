using BookingMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Service
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingMaster>> GetAvailableAsync(int bookingId);

        Task<string> CreateAsync(BookingMaster booking);
    }
}