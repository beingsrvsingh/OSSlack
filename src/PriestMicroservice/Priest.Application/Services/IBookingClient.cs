using Shared.Application.Common.Contracts.Response;

namespace Priest.Application.Service
{
    public interface IBookingClient
    {
        Task<List<BookingResponseDto>>GetBookingsByDateAsync(int entityId, DateTime date);
    }
}
