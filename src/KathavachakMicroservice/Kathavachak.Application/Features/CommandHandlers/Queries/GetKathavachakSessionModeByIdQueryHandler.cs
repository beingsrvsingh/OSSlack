using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetKathavachakSessionModeByIdQueryHandler : IRequestHandler<GetKathavachakSessionModeByIdQuery, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<GetKathavachakSessionModeByIdQueryHandler> _logger;

        public GetKathavachakSessionModeByIdQueryHandler(
            IKathavachakSessionModeService service,
            ILoggerService<GetKathavachakSessionModeByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetKathavachakSessionModeByIdQuery request, CancellationToken cancellationToken)
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
