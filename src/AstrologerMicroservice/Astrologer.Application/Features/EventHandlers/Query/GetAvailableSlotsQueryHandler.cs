using Astrologer.Application.Contracts;
using Astrologer.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.EventHandlers.Query
{
    public class GetAvailableSlotsQueryHandler: IRequestHandler<GetAvailableSlotsQuery, Result>
    {
        private readonly ILoggerService<GetAvailableSlotsQueryHandler> logger;
        private readonly IAstrologerService astrologerService;

        public GetAvailableSlotsQueryHandler(ILoggerService<GetAvailableSlotsQueryHandler> logger, IAstrologerService astrologerService)
        {
            this.logger = logger;
            this.astrologerService = astrologerService;
        }

        public async Task<Result> Handle(GetAvailableSlotsQuery request,CancellationToken cancellationToken)
        {
            var availableSlots = await astrologerService.GetTodayAvailableSlotsAsync(request.AstrologerId, request.Date);
            return Result.Success(availableSlots);
        }
    }
}
