using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetPriestByIdQueryHandler : IRequestHandler<GetPriestByIdQuery, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<GetPriestByIdQueryHandler> _logger;

        public GetPriestByIdQueryHandler(IPriestService priestService, ILoggerService<GetPriestByIdQueryHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPriestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var priest = await _priestService.GetPriestByIdAsync(request.Id);
                if (priest == null)
                    return Result.Failure($"Priest with ID {request.Id} not found.");
                return Result.Success(priest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve priest with ID {request.Id}");
                return Result.Failure("Internal error occurred.");
            }
        }
    }

}
