using MediatR;
using Shared.Utilities.Response;
using System.Text.Json;

namespace BookingMicroservice.Application.Features.Commands
{
    public class CreateBookingCommand : IRequest<Result>
    {
        //"Temple", "Priest"
        public required string EntityType { get; set; }

        public required int EntityId { get; set; }

        // Booking details
        public required string Name { get; set; }
        public required DateTime Date { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }

        public Dictionary<string, JsonElement> BookingOptions { get; set; } = new();

        // Optional notes
        public string? Notes { get; set; }
    }
}