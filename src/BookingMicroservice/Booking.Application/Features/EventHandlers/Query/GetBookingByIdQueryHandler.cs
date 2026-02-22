using BookingMicroservice.Application.Features.Query;
using BookingMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Features.EventHandlers.Query
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, Result>
    {
        private readonly IBookingService _bookingService;
        private readonly ILoggerService<GetBookingByIdQueryHandler> _logger;

        public GetBookingByIdQueryHandler(
            IBookingService astrologerService,
            ILoggerService<GetBookingByIdQueryHandler> logger)
        {
            _bookingService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching astrologer with ID: {Id}", request.Id!);
            var astrologer = await _bookingService.GetAvailableAsync(request.Id);

            if (astrologer != null)
            {
                return Result.Success(astrologer);
            }
            else
            {
                _logger.LogWarning("Booking not found for ID: {Id}", request.Id!);
                return Result.Failure(new FailureResponse("ASTRO_NOT_FOUND", "Booking not found."));
            }
        }
    }

}