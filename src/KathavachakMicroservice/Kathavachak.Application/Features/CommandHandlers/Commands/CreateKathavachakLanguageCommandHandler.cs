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
    public class CreateKathavachakLanguageCommandHandler : IRequestHandler<CreateKathavachakLanguageCommand, Result>
    {
        private readonly IKathavachakLanguageService _service;
        private readonly ILoggerService<CreateKathavachakLanguageCommandHandler> _logger;

        public CreateKathavachakLanguageCommandHandler(
            IKathavachakLanguageService service,
            ILoggerService<CreateKathavachakLanguageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakLanguage>();
                var success = await _service.CreateAsync(entity);
                return success
                    ? Result.Success("Language assigned successfully.")
                    : Result.Failure(new FailureResponse("CREATE_FAILED", "Unable to assign language."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakLanguageCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An unexpected error occurred."));
            }
        }
    }

}
