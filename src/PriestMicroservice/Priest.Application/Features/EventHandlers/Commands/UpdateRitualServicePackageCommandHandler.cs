using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdateRitualServicePackageCommandHandler : IRequestHandler<UpdateRitualServicePackageCommand, Result>
    {
        private readonly IServicePackageService _service;
        private readonly ILoggerService<UpdateRitualServicePackageCommandHandler> _logger;

        public UpdateRitualServicePackageCommandHandler(IServicePackageService service, ILoggerService<UpdateRitualServicePackageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateRitualServicePackageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.UpdateServicePackageAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update ritual service package.");
                return Result.Failure("Unable to update ritual service package.");
            }
        }
    }
}
