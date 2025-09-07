using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreatePriestExpertiseCommandHandler : IRequestHandler<CreatePriestExpertiseCommand, Result>
    {
        private readonly IPriestExpertiseService _service;
        private readonly ILoggerService<CreatePriestExpertiseCommandHandler> _logger;

        public CreatePriestExpertiseCommandHandler(IPriestExpertiseService service, ILoggerService<CreatePriestExpertiseCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreatePriestExpertiseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.CreatePriestExpertiseAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create priest expertise.");
                return Result.Failure("Unable to create priest expertise.");
            }
        }
    }

}
