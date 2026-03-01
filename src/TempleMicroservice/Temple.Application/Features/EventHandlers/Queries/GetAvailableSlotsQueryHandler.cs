using Temple.Application.Features.Query;
using Temple.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Query
{
    public class GetAvailableSlotsQueryHandler: IRequestHandler<GetAvailableSlotsQuery, Result>
    {
        private readonly ILoggerService<GetAvailableSlotsQueryHandler> logger;
        private readonly ITempleService templeService;

        public GetAvailableSlotsQueryHandler(ILoggerService<GetAvailableSlotsQueryHandler> logger, ITempleService templeService)
        {
            this.logger = logger;
            this.templeService = templeService;
        }

        public async Task<Result> Handle(GetAvailableSlotsQuery request,CancellationToken cancellationToken)
        {
            var availableSlots = await templeService.GetTodayAvailableSlotsAsync(request.EntityId, request.Date);
            return Result.Success(availableSlots);
        }
    }
}
