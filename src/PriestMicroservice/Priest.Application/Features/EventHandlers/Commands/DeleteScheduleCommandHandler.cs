using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Result>
    {
        private readonly IScheduleService _service;
        private readonly ILoggerService<DeleteScheduleCommandHandler> _logger;

        public DeleteScheduleCommandHandler(IScheduleService service, ILoggerService<DeleteScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.DeleteScheduleAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete schedule.");
                return Result.Failure("Unable to delete schedule.");
            }
        }
    }
}
