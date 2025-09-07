using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakMasterCommandHandler : IRequestHandler<DeleteKathavachakMasterCommand, Result>
    {
        private readonly IKathavachakService _service;
        private readonly ILoggerService<DeleteKathavachakMasterCommandHandler> _logger;

        public DeleteKathavachakMasterCommandHandler(
            IKathavachakService service,
            ILoggerService<DeleteKathavachakMasterCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _service.DeleteAsync(request.Id);
                return success
                    ? Result.Success("Kathavachak deleted successfully.")
                    : Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete Kathavachak."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakMasterCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
