using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTemplePrasadCommandHandler : IRequestHandler<UpdateTemplePrasadCommand, Result>
    {
        private readonly ITemplePrasadService _service;
        private readonly ILoggerService<UpdateTemplePrasadCommandHandler> _logger;

        public UpdateTemplePrasadCommandHandler(ITemplePrasadService service, ILoggerService<UpdateTemplePrasadCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTemplePrasadCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple prasad with ID: {Id}", request.Id);

            try
            {
                var prasadRequest = request.Adapt<TemplePrasad>();
                var updated = await _service.UpdateAsync(prasadRequest);
                if (updated)
                    return Result.Success("Temple prasad updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple prasad."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple prasad: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
