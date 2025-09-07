using MediatR;
using Priest.Application.Features.Commands;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Priest.Application.Services;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreatePriestLanguageCommandHandler : IRequestHandler<CreatePriestLanguageCommand, Result>
    {
        private readonly IPriestLanguageService _service;
        private readonly ILoggerService<CreatePriestLanguageCommandHandler> _logger;

        public CreatePriestLanguageCommandHandler(IPriestLanguageService service, ILoggerService<CreatePriestLanguageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreatePriestLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.CreatePriestLanguageAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create priest language.");
                return Result.Failure("Unable to create priest language.");
            }
        }
    }
}
