using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetAllKathavachakSessionModesQueryHandler : IRequestHandler<GetAllKathavachakSessionModesQuery, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<GetAllKathavachakSessionModesQueryHandler> _logger;

        public GetAllKathavachakSessionModesQueryHandler(
            IKathavachakSessionModeService service,
            ILoggerService<GetAllKathavachakSessionModesQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAllKathavachakSessionModesQuery request, CancellationToken cancellationToken)
        {
            var sessionModes = await _service.GetAllAsync();

            if (sessionModes == null || !sessionModes.Any())
            {
                _logger.LogWarning("No Kathavachak session modes found.");
                return Result.Failure(new FailureResponse("NOT_FOUND", "No Kathavachak session modes found."));
            }

            var dtos = sessionModes.Adapt<List<KathavachakSessionModeDto>>();
            return Result.Success(dtos);
        }
    }
}
