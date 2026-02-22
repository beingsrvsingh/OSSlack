using MediatR;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Features.Commands
{
    public class CreateBookingCommand : IRequest<Result>
    {
        public required int UserId { get; set; }
        public string UserDisplayName { get; set; } = string.Empty;

        // What entity is being booked (Temple, Priest, Event, etc.)
        public required string EntityType { get; set; }  // e.g., "Temple", "Priest"
        public required int EntityId { get; set; }       // the specific entity ID

        // Booking details
        public required string PoojaType { get; set; }   // or service name
        public required DateTime Date { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }

        // Optional booking source (website, mobile, API)
        public string Source { get; set; } = "website";

        // Optional notes
        public string? Notes { get; set; }
    }
}