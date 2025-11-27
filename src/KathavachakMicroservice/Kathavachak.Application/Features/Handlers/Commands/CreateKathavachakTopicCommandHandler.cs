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
    public class CreateKathavachakTopicCommandHandler : IRequestHandler<CreateKathavachakTopicCommand, Result>
    {
        private readonly IKathavachakTopicService _service;
        private readonly ILoggerService<CreateKathavachakTopicCommandHandler> _logger;

        public CreateKathavachakTopicCommandHandler(
            IKathavachakTopicService service,
            ILoggerService<CreateKathavachakTopicCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakTopic>();
                var success = await _service.CreateAsync(entity);
                return success
                    ? Result.Success("Topic created successfully.")
                    : Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create topic."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakTopicCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An unexpected error occurred."));
            }
        }
    }

}
