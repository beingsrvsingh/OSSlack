using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeleteRitualServicePackageCommandHandler : IRequestHandler<DeleteRitualServicePackageCommand, Result>
    {
        private readonly IServicePackageService _service;
        private readonly ILoggerService<DeleteRitualServicePackageCommandHandler> _logger;

        public DeleteRitualServicePackageCommandHandler(IServicePackageService service, ILoggerService<DeleteRitualServicePackageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteRitualServicePackageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.DeleteServicePackageAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete ritual service package.");
                return Result.Failure("Unable to delete ritual service package.");
            }
        }
    }
}
