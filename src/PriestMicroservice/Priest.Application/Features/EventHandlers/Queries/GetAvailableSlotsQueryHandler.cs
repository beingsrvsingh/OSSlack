using Priest.Application.Features.Query;
using Priest.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Query
{
    public class GetAvailableSlotsQueryHandler: IRequestHandler<GetAvailableSlotsQuery, Result>
    {
        private readonly ILoggerService<GetAvailableSlotsQueryHandler> logger;
        private readonly IPriestService priestService;

        public GetAvailableSlotsQueryHandler(ILoggerService<GetAvailableSlotsQueryHandler> logger, IPriestService priestService)
        {
            this.logger = logger;
            this.priestService = priestService;
        }

        public async Task<Result> Handle(GetAvailableSlotsQuery request,CancellationToken cancellationToken)
        {
            var availableSlots = await priestService.GetTodayAvailableSlotsAsync(request.EntityId, request.Date);
            return Result.Success(availableSlots);
        }
    }
}
