using MediatR;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Features.Query
{
    public record GetBookingByIdQuery(int Id) : IRequest<Result>;

}