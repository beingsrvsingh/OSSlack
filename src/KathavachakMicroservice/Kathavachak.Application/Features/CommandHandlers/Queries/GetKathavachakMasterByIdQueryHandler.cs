using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetKathavachakMasterByIdQueryHandler : IRequestHandler<GetKathavachakMasterByIdQuery, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<GetKathavachakMasterByIdQueryHandler> _logger;

        public GetKathavachakMasterByIdQueryHandler(
            IKathavachakSessionModeService service,
            ILoggerService<GetKathavachakMasterByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetKathavachakMasterByIdQuery request, CancellationToken cancellationToken)
        {
            var sessionMode = await _service.GetByIdAsync(request.Id);
            if (sessionMode == null)
            {
                _logger.LogWarning($"Kathavachak session mode not found for Id: {request.Id}");
                return Result.Failure(new FailureResponse("NOT_FOUND", $"Kathavachak session mode not found for Id: {request.Id}"));
            }

            var dto = sessionMode.Adapt<KathavachakSessionModeDto>();
            return Result.Success(dto);
        }
    }
}
