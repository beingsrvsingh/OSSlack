using Shared.Application.Common.Contracts.Response;

namespace Astrologer.Application.Service
{
    public interface IBookingClient
    {
        Task<List<BookingResponseDto>>GetBookingsByDateAsync(int astrologerId, DateTime date);
    }
}
