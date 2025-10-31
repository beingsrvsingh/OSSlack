using MediatR;
using Pooja.Application.Features.Commands;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Commands
{
    public class DeletePoojaCommandHandler : IRequestHandler<DeletePoojaCommand, Result>
    {
        private readonly IPoojaService _service;
        private readonly ILoggerService<DeletePoojaCommandHandler> _logger;

        public DeletePoojaCommandHandler(IPoojaService service, ILoggerService<DeletePoojaCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeletePoojaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeletePoojaAsync(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete pooja with id: {request.Id}");
                return Result.Failure("Unable to delete pooja.");
            }
        }
    }

}
