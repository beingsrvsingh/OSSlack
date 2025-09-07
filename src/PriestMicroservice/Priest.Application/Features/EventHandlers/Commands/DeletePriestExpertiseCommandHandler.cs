using MediatR;
using Priest.Application.Features.Commands;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Priest.Application.Services;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeletePriestExpertiseCommandHandler : IRequestHandler<DeletePriestExpertiseCommand, Result>
    {
        private readonly IPriestExpertiseService _service;
        private readonly ILoggerService<DeletePriestExpertiseCommandHandler> _logger;

        public DeletePriestExpertiseCommandHandler(IPriestExpertiseService service, ILoggerService<DeletePriestExpertiseCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeletePriestExpertiseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.DeletePriestExpertiseAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete priest expertise.");
                return Result.Failure("Unable to delete priest expertise.");
            }
        }
    }
}
