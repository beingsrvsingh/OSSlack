using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeletePriestLanguageCommandHandler : IRequestHandler<DeletePriestLanguageCommand, Result>
    {
        private readonly IPriestLanguageService _service;
        private readonly ILoggerService<DeletePriestLanguageCommandHandler> _logger;

        public DeletePriestLanguageCommandHandler(IPriestLanguageService service, ILoggerService<DeletePriestLanguageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeletePriestLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.DeletePriestLanguageAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete priest language.");
                return Result.Failure("Unable to delete priest language.");
            }
        }
    }
}
