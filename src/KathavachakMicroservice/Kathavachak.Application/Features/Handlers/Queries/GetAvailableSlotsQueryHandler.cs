using Kathavachak.Application.Features.Query;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.EventHandlers.Query
{
    public class GetAvailableSlotsQueryHandler: IRequestHandler<GetAvailableSlotsQuery, Result>
    {
        private readonly ILoggerService<GetAvailableSlotsQueryHandler> logger;
        private readonly IKathavachakService kathavachakService;

        public GetAvailableSlotsQueryHandler(ILoggerService<GetAvailableSlotsQueryHandler> logger, IKathavachakService kathavachakService)
        {
            this.logger = logger;
            this.kathavachakService = kathavachakService;
        }

        public async Task<Result> Handle(GetAvailableSlotsQuery request,CancellationToken cancellationToken)
        {
            var availableSlots = await kathavachakService.GetTodayAvailableSlotsAsync(request.EntityId, request.Date);
            return Result.Success(availableSlots);
        }
    }
}
