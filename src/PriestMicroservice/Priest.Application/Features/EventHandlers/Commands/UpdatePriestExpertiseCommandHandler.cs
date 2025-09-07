using MediatR;
using Priest.Application.Features.Commands;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Priest.Application.Services;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdatePriestExpertiseCommandHandler : IRequestHandler<UpdatePriestExpertiseCommand, Result>
    {
        private readonly IPriestExpertiseService _service;
        private readonly ILoggerService<UpdatePriestExpertiseCommandHandler> _logger;

        public UpdatePriestExpertiseCommandHandler(IPriestExpertiseService service, ILoggerService<UpdatePriestExpertiseCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePriestExpertiseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.UpdatePriestExpertiseAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update priest expertise.");
                return Result.Failure("Unable to update priest expertise.");
            }
        }
    }
}
