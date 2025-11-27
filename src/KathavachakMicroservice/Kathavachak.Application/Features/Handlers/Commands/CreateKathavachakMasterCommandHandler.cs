using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Kathavachak.Domain.Entities;
using Mapster;
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
    public class CreateKathavachakMasterCommandHandler : IRequestHandler<CreateKathavachakMasterCommand, Result>
    {
        private readonly IKathavachakService _service;
        private readonly ILoggerService<CreateKathavachakMasterCommandHandler> _logger;

        public CreateKathavachakMasterCommandHandler(
            IKathavachakService service,
            ILoggerService<CreateKathavachakMasterCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakMaster>();
                var success = await _service.CreateAsync(entity);
                return success
                    ? Result.Success("Kathavachak created successfully.")
                    : Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create Kathavachak."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakMasterCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An unexpected error occurred."));
            }
        }
    }

}
