using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakTopicCommandHandler : IRequestHandler<UpdateKathavachakTopicCommand, Result>
    {
        private readonly IKathavachakTopicService _service;
        private readonly ILoggerService<UpdateKathavachakTopicCommandHandler> _logger;

        public UpdateKathavachakTopicCommandHandler(
            IKathavachakTopicService service,
            ILoggerService<UpdateKathavachakTopicCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                if (existing == null)
                    return Result.Failure(new FailureResponse("NOT_FOUND", "Topic not found."));

                request.Adapt(existing);

                var result = await _service.UpdateAsync(existing);
                return result
                    ? Result.Success("Topic updated successfully.")
                    : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update topic."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakTopicCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
