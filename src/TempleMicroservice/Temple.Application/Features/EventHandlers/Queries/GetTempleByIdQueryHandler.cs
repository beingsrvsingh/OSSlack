using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    internal class GetTempleByIdQueryHandler : IRequestHandler<GetTempleByIdQuery, Result>
    {
        private readonly ITempleService _service;
        private readonly ILoggerService<GetTempleByIdQueryHandler> _logger;

        public GetTempleByIdQueryHandler(ITempleService service, ILoggerService<GetTempleByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple schedule with ID: {Id}", request.Id);

            var entity = await _service.GetByIdWithDetailsAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Temple schedule not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("SCHEDULE_NOT_FOUND", "Temple schedule not found."));
        }
    }
}