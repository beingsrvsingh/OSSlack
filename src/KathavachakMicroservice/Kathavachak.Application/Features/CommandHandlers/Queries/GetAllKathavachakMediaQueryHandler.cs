using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetAllKathavachakMediaQueryHandler : IRequestHandler<GetAllKathavachakMediaQuery, Result>
    {
        private readonly IKathavachakMediaService _service;
        private readonly ILoggerService<GetAllKathavachakMediaQueryHandler> _logger;

        public GetAllKathavachakMediaQueryHandler(
            IKathavachakMediaService service,
            ILoggerService<GetAllKathavachakMediaQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAllKathavachakMediaQuery request, CancellationToken cancellationToken)
        {
            var mediaList = await _service.GetAllAsync();

            if (mediaList == null || !mediaList.Any())
            {
                _logger.LogWarning("No Kathavachak media found.");
                return Result.Failure(new FailureResponse("NOT_FOUND", "No Kathavachak media found."));
            }

            var dtos = mediaList.Adapt<List<KathavachakMediaDto>>();
            return Result.Success(dtos);
        }
    }
}
