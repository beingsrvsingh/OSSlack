using Shared.Application.Common.Contracts.Response;

namespace Kathavachak.Application.Service
{
    public interface IBookingClient
    {
        Task<List<BookingResponseDto>>GetBookingsByDateAsync(int entityId, DateTime date);
    }
}
