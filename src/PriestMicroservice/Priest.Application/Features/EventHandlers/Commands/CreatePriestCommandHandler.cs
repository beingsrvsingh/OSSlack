using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreatePriestCommandHandler : IRequestHandler<CreatePriestCommand, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<CreatePriestCommandHandler> _logger;

        public CreatePriestCommandHandler(IPriestService priestService, ILoggerService<CreatePriestCommandHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(CreatePriestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var priest = new PriestMaster
                {
                    UserId = request.UserId,
                    Name = request.DisplayName,
                    ThumbnailUrl = request.ProfilePictureUrl,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _priestService.AddPriestAsync(priest);
                return Result.Success(priest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create priest.");
                return Result.Failure("Error creating priest.");
            }
        }
    }

}
