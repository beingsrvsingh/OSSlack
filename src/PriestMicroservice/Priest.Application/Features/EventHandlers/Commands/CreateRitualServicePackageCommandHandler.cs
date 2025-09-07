using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreateRitualServicePackageCommandHandler : IRequestHandler<CreateRitualServicePackageCommand, Result>
    {
        private readonly IServicePackageService _service;
        private readonly ILoggerService<CreateRitualServicePackageCommandHandler> _logger;

        public CreateRitualServicePackageCommandHandler(IServicePackageService service, ILoggerService<CreateRitualServicePackageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateRitualServicePackageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.CreateServicePackageAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create ritual service package.");
                return Result.Failure("Unable to create ritual service package.");
            }
        }
    }
}
