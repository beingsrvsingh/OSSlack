using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdatePriestLanguageCommandHandler : IRequestHandler<UpdatePriestLanguageCommand, Result>
    {
        private readonly IPriestLanguageService _service;
        private readonly ILoggerService<UpdatePriestLanguageCommandHandler> _logger;

        public UpdatePriestLanguageCommandHandler(IPriestLanguageService service, ILoggerService<UpdatePriestLanguageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePriestLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.UpdatePriestLanguageAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update priest language.");
                return Result.Failure("Unable to update priest language.");
            }
        }
    }
}
