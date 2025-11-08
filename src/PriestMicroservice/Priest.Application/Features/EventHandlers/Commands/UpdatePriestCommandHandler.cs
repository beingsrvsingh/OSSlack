using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdatePriestCommandHandler : IRequestHandler<UpdatePriestCommand, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<UpdatePriestCommandHandler> _logger;

        public UpdatePriestCommandHandler(IPriestService priestService, ILoggerService<UpdatePriestCommandHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePriestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var priest = await _priestService.GetPriestByIdAsync(request.Id);
                if (priest == null)
                    return Result.Failure($"Priest with ID {request.Id} not found.");

                priest.Name = request.DisplayName ?? priest.Name;
                priest.ThumbnailUrl = request.ProfilePictureUrl ?? priest.ThumbnailUrl;

                //await _priestService.UpdatePriestAsync(priest);
                return Result.Success(priest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update priest with ID {request.Id}");
                return Result.Failure("Error updating priest.");
            }
        }
    }

}
