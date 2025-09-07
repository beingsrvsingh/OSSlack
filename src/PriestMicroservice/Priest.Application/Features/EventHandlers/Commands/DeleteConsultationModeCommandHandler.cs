using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeleteConsultationModeCommandHandler : IRequestHandler<DeleteConsultationModeCommand, Result>
    {
        private readonly IConsultationModeService _service;
        private readonly ILoggerService<DeleteConsultationModeCommandHandler> _logger;

        public DeleteConsultationModeCommandHandler(IConsultationModeService service, ILoggerService<DeleteConsultationModeCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteConsultationModeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.DeleteConsultationModeAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete consultation mode.");
                return Result.Failure("Unable to delete consultation mode.");
            }
        }
    }
}
