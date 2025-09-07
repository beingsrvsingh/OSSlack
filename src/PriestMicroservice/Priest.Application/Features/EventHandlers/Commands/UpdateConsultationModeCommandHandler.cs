using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdateConsultationModeCommandHandler : IRequestHandler<UpdateConsultationModeCommand, Result>
    {
        private readonly IConsultationModeService _service;
        private readonly ILoggerService<UpdateConsultationModeCommandHandler> _logger;

        public UpdateConsultationModeCommandHandler(IConsultationModeService service, ILoggerService<UpdateConsultationModeCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateConsultationModeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.UpdateConsultationModeAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update consultation mode.");
                return Result.Failure("Unable to update consultation mode.");
            }
        }
    }

}
