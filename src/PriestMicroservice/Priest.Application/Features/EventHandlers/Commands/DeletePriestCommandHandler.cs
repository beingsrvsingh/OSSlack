using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeletePriestCommandHandler : IRequestHandler<DeletePriestCommand, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<DeletePriestCommandHandler> _logger;

        public DeletePriestCommandHandler(IPriestService priestService, ILoggerService<DeletePriestCommandHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(DeletePriestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _priestService.DeletePriestAsync(request.Id);
                return Result.Success($"Priest with ID {request.Id} deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete priest with ID {request.Id}");
                return Result.Failure("Error deleting priest.");
            }
        }
    }

}
