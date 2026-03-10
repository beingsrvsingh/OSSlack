using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetPriestExpertiseModeIdPriceQueryHandler : IRequestHandler<GetPriestExpertiseModeIdPriceQuery, Result>
    {
        private readonly ILoggerService<GetPriestExpertiseModeIdPriceQueryHandler> loggerService;
        private readonly IConsultationModeService consultationModeService;

        public GetPriestExpertiseModeIdPriceQueryHandler(ILoggerService<GetPriestExpertiseModeIdPriceQueryHandler> loggerService, IConsultationModeService consultationModeService)
        {
            this.loggerService = loggerService;
            this.consultationModeService = consultationModeService;
        }

        public async Task<Result> Handle(GetPriestExpertiseModeIdPriceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var basePrice = await consultationModeService.GetPriestExpertiseModeIdPrice(request.PriestExpertiseId, request.ModeId);
                if (basePrice == null)
                    return Result.Failure(new FailureResponse("INTERNAL_SERVER_ERROR", "Something went wrong."));
                return Result.Success(basePrice);
            }
            catch (Exception ex)
            {
                loggerService.LogError(ex, $"Failed to retrieve priest with ID {request.PriestExpertiseId}");
                return Result.Failure(new FailureResponse("INTERNAL_SERVER_ERROR", "Something went wrong."));
            }
        }
    }
}
