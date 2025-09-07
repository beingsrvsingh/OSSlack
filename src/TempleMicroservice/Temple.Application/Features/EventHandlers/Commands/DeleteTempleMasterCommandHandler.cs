using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleMasterCommandHandler : IRequestHandler<DeleteTempleMasterCommand, Result>
    {
        private readonly ITempleService _service;
        private readonly ILoggerService<DeleteTempleMasterCommandHandler> _logger;

        public DeleteTempleMasterCommandHandler(ITempleService service, ILoggerService<DeleteTempleMasterCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple master with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple master deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple master."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple master: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
