using Shared.Application.Common.Contracts.Response;

namespace Temple.Application.Service
{
    public interface IBookingClient
    {
        Task<List<BookingResponseDto>>GetBookingsByDateAsync(int entityId, DateTime date);
    }
}
