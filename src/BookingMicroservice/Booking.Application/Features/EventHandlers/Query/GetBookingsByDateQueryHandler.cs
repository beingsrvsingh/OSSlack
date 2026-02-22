using BookingMicroservice.Application.Features.Query;
using BookingMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace BookingMicroservice.Application.Features.EventHandlers.Query
{
    public class GetBookingsByDateQueryHandler: IRequestHandler<GetBookingsByDateQuery, Result>
    {
        private readonly ILoggerService<GetBookingsByDateQueryHandler> logger;
        private readonly IBookingService bookingService;

        public GetBookingsByDateQueryHandler(ILoggerService<GetBookingsByDateQueryHandler> logger, IBookingService bookingService)
        {
            this.logger = logger;
            this.bookingService = bookingService;
        }

        public async Task<Result> Handle(GetBookingsByDateQuery request,CancellationToken cancellationToken)
        {
            var availableSlots = await bookingService.GetBookingsByDateAsync(request.EntityId, request.Date);
            return Result.Success(availableSlots);
        }
    }
}
