using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreateConsultationModeCommandHandler : IRequestHandler<CreateConsultationModeCommand, Result>
    {
        private readonly IConsultationModeService _service;
        private readonly ILoggerService<CreateConsultationModeCommandHandler> _logger;

        public CreateConsultationModeCommandHandler(IConsultationModeService service, ILoggerService<CreateConsultationModeCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateConsultationModeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.CreateConsultationModeAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create consultation mode.");
                return Result.Failure("Unable to create consultation mode.");
            }
        }
    }
}
