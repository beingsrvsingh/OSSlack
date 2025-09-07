using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetAllConsultationModesQueryHandler : IRequestHandler<GetAllConsultationModesQuery, Result>
    {
        private readonly ILoggerService<GetAllConsultationModesQueryHandler> loggerService;
        private readonly IConsultationModeService consultationModeService;

        public GetAllConsultationModesQueryHandler(ILoggerService<GetAllConsultationModesQueryHandler> loggerService, IConsultationModeService consultationModeService)
        {
            this.loggerService = loggerService;
            this.consultationModeService = consultationModeService;
        }
        public async Task<Result> Handle(GetAllConsultationModesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modes = await consultationModeService.GetAllAsync();

                if (modes == null || !modes.Any())
                    return Result.Failure(new FailureResponse("NotFound", "No consultation modes found."));

                return Result.Success(modes);
            }
            catch (Exception ex)
            {
                loggerService.LogError(ex, "Failed to fetch consultation modes.");
                return Result.Failure(new FailureResponse("ServerError", "Unable to retrieve consultation modes."));
            }
        }
    }
}
