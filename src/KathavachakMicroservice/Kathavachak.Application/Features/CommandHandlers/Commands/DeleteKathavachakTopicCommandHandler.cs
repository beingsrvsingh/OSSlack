using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakTopicCommandHandler : IRequestHandler<DeleteKathavachakTopicCommand, Result>
    {
        private readonly IKathavachakTopicService _service;
        private readonly ILoggerService<DeleteKathavachakTopicCommandHandler> _logger;

        public DeleteKathavachakTopicCommandHandler(
            IKathavachakTopicService service,
            ILoggerService<DeleteKathavachakTopicCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _service.DeleteAsync(request.Id);
                return success
                    ? Result.Success("Topic deleted successfully.")
                    : Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete topic."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakTopicCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
