using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetKathavachakCategoryByIdQueryHandler : IRequestHandler<GetKathavachakCategoryByIdQuery, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<GetKathavachakCategoryByIdQueryHandler> _logger;

        public GetKathavachakCategoryByIdQueryHandler(
            IKathavachakSessionModeService service,
            ILoggerService<GetKathavachakCategoryByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetKathavachakCategoryByIdQuery request, CancellationToken cancellationToken)
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
