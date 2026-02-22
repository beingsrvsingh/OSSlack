using MediatR;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Features.Query
{
    public record GetBookingsByDateQuery(int EntityId, DateTime Date): IRequest<Result>;
}
